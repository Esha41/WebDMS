using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.DocumentApproved;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
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
    public class DocumentSearchController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDocumentApprovedService _documentApprovedService;
        private readonly ICompanyRepository<DocumentSearchView> _viewCompanyRepository;

        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DocumentSearchController(DMSContext context,
            IMapper mapper,
            ILogger<DocumentSearchController> logger,
            IDocumentApprovedService documentApprovedService)
        {
            _viewCompanyRepository = new CompanyEntityRepository<DocumentSearchView>(context);
            _mapper = mapper;
            _logger = logger;
            _documentApprovedService = documentApprovedService;
        }

        /// <summary>
        /// Gets the DocumentSearchView w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult GetDocumentSearchedList([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get DocumentSearched Called with params: {0}", queryStringParams);

            PagedResult<DocumentSearchViewDTO> result;
            try
            {
                QueryResult<DocumentSearchView> queryResult = new();
                if (queryStringParams.FilterExpression != null)
                {
                    if (queryStringParams.FilterExpression.Contains("ExpirationDate"))
                    {
                        string filter = queryStringParams.FilterExpression.ToString();
                        filter = filter.Replace("00", "00000");
                        queryResult = _viewCompanyRepository.Get(
                                             CompanyIds,
                                             filter,
                                             queryStringParams.OrderBy,
                                             queryStringParams.PageSize,
                                             queryStringParams.PageNumber);
                    }
                    else
                    {
                        queryResult = _viewCompanyRepository.Get(
                                       CompanyIds,
                                       queryStringParams.FilterExpression,
                                       queryStringParams.OrderBy,
                                       queryStringParams.PageSize,
                                       queryStringParams.PageNumber);
                    }
                }
                else
                {
                    queryResult = _viewCompanyRepository.Get(
                                         CompanyIds,
                                         queryStringParams.FilterExpression,
                                         queryStringParams.OrderBy,
                                         queryStringParams.PageSize,
                                         queryStringParams.PageNumber);
                }
                queryResult.List = queryResult.List.ToList();

                List<DocumentSearchViewDTO> list = _mapper.Map<List<DocumentSearchViewDTO>>(queryResult.List);

                int total = queryResult.Count;
                int lastmodifiedById = 0;
                foreach (var item in list)
                {
                    // Check Document Approved
                    bool documentApproved = _documentApprovedService.checkDocumentApproved(item.Id);

                    // Check last modified By UserId
                    lastmodifiedById = Convert.ToInt32(item.LastModifiedById);

                    // if  last modified By UserId Equal to Current Login UserId
                    // Or Document Approved 
                    // Or Current Login User Not Have Role Access to Checkout Document
                    if (lastmodifiedById == UserId ||
                        _documentApprovedService.checkDocumentApproved(item.Id) ||
                        !_documentApprovedService.checkUserRoleHaveDocumentCheckoutAccess(item.Id))
                    {
                        //Disabled Checkout Option
                        item.isNothaveAccessToCheckedOut = true;
                    }

                    // if Document Approved 
                    // Or Current Login User Not Have Role Access to Checkout Document
                    else if (documentApproved ||
                         !_documentApprovedService.checkUserRoleHaveDocumentCheckoutAccess(item.Id))
                    {
                        //Disabled Checkout Option
                        item.isNotHaveRoleAccessOrDocumentApproved = true;
                        item.CheckedOutBy = "Not Allowed";
                    }



                }
                result = new PagedResult<DocumentSearchViewDTO>(
                                 total,
                                 queryStringParams.PageNumber,
                                 list,
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
    }
}

