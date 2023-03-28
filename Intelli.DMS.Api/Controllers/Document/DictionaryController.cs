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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DictionaryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DictionaryType> _repositoryDictionaryType;
        private readonly IRepository<BopDictionary> _repositoryBopDictionary;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentCategoryController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DictionaryController(DMSContext context,
            IMapper mapper,
            ILogger<DictionaryController> logger,
            IEventSender sender)
        {
            _repositoryDictionaryType = new GenericRepository<DictionaryType>(context);
            _repositoryBopDictionary = new GenericRepository<BopDictionary>(context);

            ((GenericRepository<DictionaryType>)_repositoryDictionaryType).AfterSave =
            ((GenericRepository<BopDictionary>)_repositoryBopDictionary).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));


            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All DictionaryType.
        /// </summary>
        /// <returns>List of DictionaryType</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new DictionaryTypeDTO();
            var result = await _repositoryDictionaryType.GetAllActiveAsync(nameof(dto.DictionaryTypeName));
            return Ok(new
            {
                Items = result.List.Select(x => _mapper.Map<DictionaryTypeDTO>(x))
                .Select(s => new { s.Id, s.DictionaryTypeName })
                .ToList()
            });
        }

        /// <summary>
        /// Get All bopDictionary with dictionaryTypeId.
        /// </summary>
        /// <param name="dictionaryTypeId">The DictionaryType Id.</param>
        /// <returns>List of BopDictionaryDTO</returns>
        [HttpGet("bopDictionary/{dictionaryTypeId}")]
        public IActionResult GetAllBopDictionary(int dictionaryTypeId)
        {
            try
            {
                var result = _repositoryBopDictionary.Query(filter: s => s.DictionaryTypeId == dictionaryTypeId)
                                                            .OrderBy(x => x.DictionaryTypeId);

                return Ok(new { Items = result.Select(x => _mapper.Map<BopDictionaryDTO>(x)).ToList() });
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
