using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The Countries Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public CountriesController(DMSContext context,
            IMapper mapper,
            IEventSender sender)
        {
            _repository = new GenericRepository<Country>(context);

            ((GenericRepository<Country>)_repository).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of CompanyDTO</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new CountryDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.CountryName));
            return Ok(new { Items = result.List.Select(x => _mapper.Map<CountryDTO>(x)).ToList() });
        }
    }
}
