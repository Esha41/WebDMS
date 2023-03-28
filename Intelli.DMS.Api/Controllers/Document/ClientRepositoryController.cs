using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services;
using Intelli.DMS.Api.Services.ClientRepository;
using Intelli.DMS.Api.Services.DocumentCheckOut;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRepositoryController : BaseController
    {

        private readonly string _repositoryRoot;
        private readonly ILogger _logger;
        private readonly IClientRepositoryService _ClientRepositoryService;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly ICompanyRepository<ClientRepositoryView> _companySpecificClientRepository;
        private readonly IDocumentUrlService _documentUrlService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepositoryController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        /// <param name="documentUrlService">Instance of <see cref="IDocumentUrlService"/> will be injected</param>
        /// <param name="checkOutService">Instance of <see cref="ICheckOutService"/> will be injected</param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/> will be injected</param>
        public ClientRepositoryController(DMSContext context,
            ILogger<ClientRepositoryController> logger,
            IEventSender sender,
            IDocumentUrlService documentUrlService,
            IClientRepositoryService clientRepositoryService,
            IConfiguration configuration)
        {
            _companySpecificClientRepository = new CompanyEntityRepository<ClientRepositoryView>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _ClientRepositoryService = clientRepositoryService;
            ((GenericRepository<Batch>)_repositoryBatch).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            _configuration = configuration;
            _repositoryRoot = _configuration.GetSection("DocumentUploadPath").Value;
            _logger = logger;
            _documentUrlService = documentUrlService;
        }

        /// <summary>
        /// Gets the Customer w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Repository Called with params: {0}", queryStringParams);

            PagedResult<ClientRepositoryView> result;
            try
            {
                var queryResult = _companySpecificClientRepository.Get(
                                        CompanyIds,
                                        queryStringParams.FilterExpression,
                                        queryStringParams.OrderBy,
                                        queryStringParams.PageSize,
                                        queryStringParams.PageNumber);

                queryResult.List = queryResult.List.ToList();


                var clientRepositoryList = queryResult.List.Select(x => x.Id).ToList();

                int total = queryResult.Count;


                result = new PagedResult<ClientRepositoryView>(
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
        /// Gets the ClientDTO With repositoryId.
        /// </summary>
        /// <param name="repositoryId">The Repository Id.</param>
        /// <returns>The ClientDTO.</returns>
        [HttpGet("{repositoryId}")]
        public IActionResult GetByRepositoryId(int repositoryId)
        {
            _logger.LogInformation("Get Repository With Id: {0}", repositoryId);
            try
            {
                if (repositoryId <= 0)
                {
                    return BadRequest(MsgKeys.NotAllowed);
                }
                var query = _companySpecificClientRepository.Query(CompanyIds, x => x.Id == repositoryId);
                var entity = query?.FirstOrDefault();
                if (entity == null)
                {
                    return Ok();
                }
                else
                {
                    ClientDTO clientDTO = _ClientRepositoryService.GetClientDetailWithRepositoryId(repositoryId, entity);
                    return Ok(clientDTO);
                }
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

        }

        /// <summary>
        /// Gets the DocumentVersions With documentId.
        /// </summary>
        /// <param name="documentId">The Document Id.</param>
        /// <returns>List of  DocumentVersionListDTO.</returns>
        [HttpGet("DocumentVersions/{documentId}")]
        public IActionResult GetDocumentVersion(int documentId)
        {
            try
            {
                if (documentId < 0)
                {
                    return BadRequest(MsgKeys.NotAllowed);
                }
                List<DocumentVersionListDTO> documentVersionListDTOs = _ClientRepositoryService.MakeDocumentVersionList(documentId);
                return Ok(new
                {
                    DocumentVersions = documentVersionListDTOs,
                });
            }
            catch (Exception ex)
            {
                // Log error message
                _logger.LogError("{0}: {1}", ex.Message, ex);

                return BadRequest(new
                {
                    Error = ex,
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Gets the DocumentFields With batchItemId and documentVersionId.
        /// </summary>
        /// <param name="batchItemId">The Document Id.</param>
        /// <returns>List of  DocumentFieldValueDTO and Client Information</returns>
        [HttpGet("DocumentFields/Values/{batchItemId}")]
        public IActionResult GetDocumentFieldValues(int batchItemId)
        {
            try
            {
                if (batchItemId <= 0)
                {
                    return BadRequest(MsgKeys.NotAllowed);
                }

                _ClientRepositoryService.GetDocumentFieldValues(batchItemId,
                                       out BatchItem entity,
                                       out Client customer,
                                       out DocumentClass documentClass,
                                       out bool isDocumentClassModifiable,
                                       out List<DocumentFieldValueDTO> documentFieldValueDTOs);

                var filePath = _documentUrlService.GetDocumentUrlByBatchItemId(batchItemId).Result;

                return Ok(new
                {
                    Fields = documentFieldValueDTOs,
                    ClientName = $"{customer?.FirstName} {customer?.LastName}",
                    ClientId = customer.Id,
                    DocumentId = batchItemId,
                    DocumentName = entity?.FileName.ToString(),
                    URL = filePath,
                    DocumentClassName = documentClass?.DocumentClassName.ToString(),
                    DocumentClassId = documentClass?.Id,
                    entity?.CompanyId,
                    isDocumentClassModifiable,
                    JMBG = customer.JMBG
                });
            }
            catch (Exception ex)
            {
                // Log error message
                _logger.LogError("{0}: {1}", ex.Message, ex);
                return BadRequest(new
                {
                    Error = ex,
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Get All documents with client name.
        /// </summary>
        /// <param name="clientId">The Client Id.</param>
        /// <returns>ClientDocumentsDTO</returns>
        [HttpGet("GetClientDocuments/{clientId}")]
        public IActionResult GetClientDocuments(int clientId)
        {
            try
            {
                ClientDocumentsOldDTO response = _ClientRepositoryService.GetClientDocumentWithClientId(clientId,
                                                                                                     _repositoryRoot);

                return Ok(new
                {
                    Items = response
                });
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
        }

        /// <summary>
        /// Get All documents with client name.
        /// </summary>
        /// <param name="clientId">The Client Id.</param>
        /// <returns>ClientDocumentsDTO</returns>
        [HttpGet("GetDocumentCheckOutHistory/{documentId}")]
        public IActionResult GetDocumentCheckOutHistory(int documentId)
        {
            try
            {
                List<DocumentCheckOutLogsView> response = _ClientRepositoryService.GetDocumentCheckoutHistory(documentId);

                return Ok(new
                {
                    Items = response
                });
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
        }

    }
}
