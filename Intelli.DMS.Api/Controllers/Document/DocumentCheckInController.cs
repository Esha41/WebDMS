using AutoMapper;
using Intelli.DMS.Api.Services.DocumentCheckIn;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
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
    public class DocumentCheckInController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICheckInService _checkInService;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentCheckInController"/> class.
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="checkOutService">Instance of <see cref="ICheckInService"/> will be injected</param>
        public DocumentCheckInController(ICheckInService checkInService,
             IMapper mapper, DMSContext context,
             IEventSender sender,
            ILogger<DocumentCheckInController> logger)
        {
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _checkInService = checkInService;
            _logger = logger;
            _mapper = mapper;

            ((GenericRepository<DocumentsCheckedOut>)_repositoryDocumentsCheckedOut).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

        }

        /// <summary>
        /// Checked in a document.
        /// </summary>
        /// <param name="batchItemId">batchItemId relevant to document.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{batchItemId}")]
        public async Task<IActionResult> Put(int batchItemId) //isActive false
        {
            try
            {
                //return 0 if checkedIn is not allowed otherwise an Id of DocumentsCheckedOut
                var getValue = _checkInService.IsCheckInAllowed(batchItemId, UserId);

                //doc can only be checked in if same user has checked out it
                if (getValue == 0)
                    return BadRequest(MsgKeys.NotAllowed);

                //calling service to checkIn a document
                await _checkInService.CheckInDocument(batchItemId, getValue);

                return Ok(message: "Document Checked In");
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
