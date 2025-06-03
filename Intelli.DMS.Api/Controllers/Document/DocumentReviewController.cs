using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.DocumentApproved;
using Intelli.DMS.Api.Services.DocumentReview;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentReviewController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDocumentApprovedService _documentApprovedService;
        private readonly ICompanyRepository<DocumentReviewView> _companySpecificViewRepository;
        private readonly IRepository<DocumentReviewView> _viewRepository;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IDocumentReviewService _documentReviewService;
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentReviewController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DocumentReviewController(DMSContext context,
                                        IMapper mapper,
                                        IDocumentApprovedService documentApprovedService,
                                        ILogger<DocumentReviewController> logger,
                                        IDocumentReviewService documentReviewService)
        {
            _companySpecificViewRepository = new CompanyEntityRepository<DocumentReviewView>(context);
            _viewRepository = new GenericRepository<DocumentReviewView>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);
            _documentReviewService = documentReviewService;
            _documentApprovedService = documentApprovedService;
            _mapper = mapper;
            _logger = logger;


        }

        /// <summary>
        /// Gets the DocumentReviewView w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get DocumentReview Called with params: {0}", queryStringParams);

            PagedResult<DocumentReviewPaginationDTO> result = null;
            try
            {
                var userRoles = _repositorySystemUserRole.Query(x => x.SystemUserId == UserId)
                                                        .Select(x => x.SystemRoleId).ToList();

                var query = _companySpecificViewRepository.Query(CompanyIds, x => userRoles.Contains(x.CurrentReviewRole));

                var queryResult = _viewRepository.GetPaginatedByQuery(query,
                                                                      queryStringParams.FilterExpression,
                                                                      queryStringParams.OrderBy,
                                                                      queryStringParams.PageSize,
                                                                      queryStringParams.PageNumber);

                List<DocumentReviewPaginationDTO> documentReviewPaginationDTOs =
                                queryResult.List.Select(x => _mapper.Map<DocumentReviewPaginationDTO>(x)).ToList();


                int lastmodifiedById = 0;
                foreach (var item in documentReviewPaginationDTOs)
                {
                    // Check Document Approved
                    bool documentApproved = _documentApprovedService.checkDocumentApproved(item.Id);

                    // Check last modified By UserId
                    lastmodifiedById = Convert.ToInt32(item.LastModifiedById);

                    // if  last modified By UserId Equal to Current Login UserId
                    // Or Document Approved 
                    if (lastmodifiedById == UserId || documentApproved)
                    {
                        // Checkout Option Disabled
                        item.IsLastModifiedByYou = true;
                        item.CheckedOutBy = "Not Allowed";
                    }

                }
                int total = queryResult.Count;

                result = new PagedResult<DocumentReviewPaginationDTO>(+
                                total,
                                queryStringParams.PageNumber,
                                documentReviewPaginationDTOs,
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

        // <summary>
        /// Get All documents of pending review and which are not checked out yet.
        /// </summary>
        /// <returns>List of documents to be reviewer</returns>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                //getting list of documents except "reviewed documents" 
                var result = _documentReviewService.GetPendingReviewDoc();

                return Ok(new
                {
                    Items = result.ToList()
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
