using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The Audit api controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuditController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<Audit> _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditController"/> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        /// <param name="logger">The logger.</param>
        public AuditController(DMSAuditContext context, ILogger<AuditController> logger)
        {
            _auditRepository = new GenericRepository<Audit>(context);
            _logger = logger;
        }

        /// <summary>
        /// Gets the Audit.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of Response object.</returns>
        [HttpGet]
        public ActionResult<Response> GetAudits([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Audit log Called with params: {0}", queryStringParams);
            PagedResult<Audit> result;

            try
            {
                var queryResult = _auditRepository.Get(
                                       queryStringParams.FilterExpression,
                                       queryStringParams.OrderBy,
                                       queryStringParams.PageSize,
                                       queryStringParams.PageNumber );

                int total = queryResult.Count;
                var auditList = queryResult.List;

                result = new PagedResult<Audit>(
                        total,
                        queryStringParams.PageNumber,
                        auditList,
                        queryStringParams.PageSize
                    );
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { Errors = e, e.Message });
            }

            return Ok(result);
        }
    }
}
