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

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentClassFieldsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DocumentClassField> _repositoryDocumentClassField;
        private readonly IRepository<DocumentClassFieldType> _repositoryDocumentClassFieldType;
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentClassFieldsController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DocumentClassFieldsController(DMSContext context,
            IMapper mapper,
            ILogger<DocumentClassFieldsController> logger,
            IEventSender sender)
        {
            _repositoryDocumentClassField = new GenericRepository<DocumentClassField>(context);
            _repositoryDocumentClassFieldType = new GenericRepository<DocumentClassFieldType>(context);

            ((GenericRepository<DocumentClassFieldType>)_repositoryDocumentClassFieldType).AfterSave =
            ((GenericRepository<DocumentClassField>)_repositoryDocumentClassField).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All DocumentClassField.
        /// </summary>
        /// <returns>List of DocumentClassFieldType</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new DocumentClassFieldTypeDTO();
            var result = await _repositoryDocumentClassFieldType.GetAllActiveAsync(nameof(dto.Type));
            return Ok(new
            {
                Items = result.List.Select(x => _mapper.Map<DocumentClassFieldTypeDTO>(x)).ToList()
            });
        }
    }
}