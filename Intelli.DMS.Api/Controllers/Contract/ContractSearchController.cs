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
    public class ContractSearchController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDocumentApprovedService _documentApprovedService;
        private readonly ICompanyRepository<ContractSearchView> _viewCompanyRepository;

        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public ContractSearchController(DMSContext context,
            IMapper mapper,
            ILogger<DocumentSearchController> logger,
            IDocumentApprovedService documentApprovedService)
        {
            _viewCompanyRepository = new CompanyEntityRepository<ContractSearchView>(context);
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
        public IActionResult GetContractSearchedList([FromQuery] QueryStringParams queryStringParams)
        { 
            _logger.LogInformation("Get ContractSearched Called with params: {0}", queryStringParams);

            PagedResult<ContractSearchViewDTO> result;
            try
            {
                QueryResult<ContractSearchView> queryResult = new();
                if (queryStringParams.FilterExpression != null)
                {
                    
                        queryResult = _viewCompanyRepository.Get(
                                       CompanyIds,
                                       queryStringParams.FilterExpression,
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
                queryResult.List = queryResult.List.ToList();

                List<ContractSearchViewDTO> list = _mapper.Map<List<ContractSearchViewDTO>>(queryResult.List);

                int total = queryResult.Count;
                int lastmodifiedById = 0;
                result = new PagedResult<ContractSearchViewDTO>(
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

