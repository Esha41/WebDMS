using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Core.Repository;
using Microsoft.Extensions.Logging;
using Intelli.DMS.Shared;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The Logging Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoggingController : BaseController
    {
        private readonly DMSContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<Nlog> _repositoryNLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public LoggingController(DMSContext context,
            IMapper mapper, ILogger<LoggingController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _repositoryNLog = new GenericRepository<Nlog>(context);
        }

        /// <summary>
        /// Gets the System Logs.
        /// </summary>
        /// <returns>SystemLogDTO.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Logging Called with params: {0}", queryStringParams);
            PagedResult<SystemLogDTO> pagedResult;
            try
            {
                QueryResult<Nlog> queryResult = _repositoryNLog.Get(
                                                queryStringParams.FilterExpression,
                                                queryStringParams.OrderBy,
                                                queryStringParams.PageSize,
                                                queryStringParams.PageNumber);

                int totalCount = queryResult.Count;
                List<Nlog> List = queryResult.List;
                pagedResult = new PagedResult<SystemLogDTO>(
                                    totalCount,
                                    queryStringParams.PageNumber,
                                    List.Select(x => _mapper.Map<SystemLogDTO>(x)).ToList(),
                                    queryStringParams.PageSize);
            }
            catch (ArgumentException e)
            {
                //Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(pagedResult);
        }
    }
}
