using AutoMapper;
using Intelli.DMS.Api.Services.DocumentCheckOut;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentCheckOutController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICheckOutService _checkOutService;
        private readonly IRepository<DocumentCheckOutView> _repositoryDocumentsCheckedOut;
        private readonly ICompanyRepository<DocumentCheckOutView> _companySpecificRepositoryDocumentsCheckedOut;
        private readonly IRepository<BatchItemStatus> _repositoryBatchItemStatus;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<Client> _repositoryClient;


        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentCheckOutController"/> class.
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="checkOutService">Instance of <see cref="ICheckOutService"/> will be injected</param>
        public DocumentCheckOutController(ICheckOutService checkOutService,
             IMapper mapper, DMSContext context, IEventSender sender,
            ILogger<DocumentCheckOutController> logger)
        {
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentCheckOutView>(context);
            _companySpecificRepositoryDocumentsCheckedOut = new CompanyEntityRepository<DocumentCheckOutView>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchItemStatus = new GenericRepository<BatchItemStatus>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryClient = new GenericRepository<Client>(context);
            _checkOutService = checkOutService;
            _logger = logger;
            _mapper = mapper;

            ((GenericRepository<Client>)_repositoryClient).AfterSave = (logs) =>
            ((GenericRepository<Batch>)_repositoryBatch).AfterSave = (logs) =>
            ((GenericRepository<BatchItemStatus>)_repositoryBatchItemStatus).AfterSave = (logs) =>
            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
            ((GenericRepository<DocumentsCheckedOut>)_repositoryDocumentsCheckedOut).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

        }

        /// <summary>
        /// Gets the DocumentCheckOutView w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get CheckedOutDocuments Called with params: {0}", queryStringParams);

            PagedResult<DocumentCheckOutView> result = null;
            try
            {
                var query = _companySpecificRepositoryDocumentsCheckedOut.Query(CompanyIds, x => x.UserId == UserId);

                var queryResult = _repositoryDocumentsCheckedOut.GetPaginatedByQuery(query,
                                                                                    queryStringParams.FilterExpression,
                                                                                    queryStringParams.OrderBy,
                                                                                    queryStringParams.PageSize,
                                                                                    queryStringParams.PageNumber);


                int total = queryResult.Count;

                result = new PagedResult<DocumentCheckOutView>(
                                total,
                                queryStringParams.PageNumber,
                                queryResult.List,
                                queryStringParams.PageSize
                            );
            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Checked out a document.
        /// </summary>
        /// <param name="batchItemId">batchItemId relevant to document.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("UpdateCheckout/{batchItemId}")]
        public async Task<IActionResult> UpdateCheckout(int batchItemId)
        {
            try
            {
                if (batchItemId < 0)
                {
                    return BadRequest(MsgKeys.NotAllowed);
                }
                //if any user has checked out a document then no other user can check-out 
                var checkStatus = _checkOutService.IsDocumentCheckOut(batchItemId);

                if (checkStatus != 0)
                    return Ok(new
                    {
                        CheckoutId = checkStatus,
                    });

                //calling service to checkOut a document
                var result = await _checkOutService.CheckOutDocument(batchItemId);

                if (result.BatchItemId == 0)
                {
                    return BadRequest(MsgKeys.NotAllowed);
                }

                return Ok(new
                {
                    CheckoutId = result.Id,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

    }
}
