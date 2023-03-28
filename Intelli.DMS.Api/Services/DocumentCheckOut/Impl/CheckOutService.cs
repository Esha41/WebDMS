using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.Controllers.Document;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentCheckOut.Impl
{
    public class CheckOutService : BaseService, ICheckOutService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;
        private readonly IRepository<DocumentsCheckedOutLog> _repositoryDocumentsCheckedOutLog;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckOutService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        /// 
        public CheckOutService(DMSContext context,
           ILogger<CheckOutService> logger,
           IEventSender sender, IMapper mapper,
           IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _repositoryDocumentsCheckedOutLog = new GenericRepository<DocumentsCheckedOutLog>(context);
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);

            ((GenericRepository<DocumentsCheckedOut>)_repositoryDocumentsCheckedOut).AfterSave = 
            ((GenericRepository<DocumentsCheckedOutLog>)_repositoryDocumentsCheckedOutLog).AfterSave = 
            ((GenericRepository<BatchMetum>)_repositoryBatchMeta).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Checked out a document.
        /// </summary>
        /// <param name="batchItemId">batchItemId relevant to document.</param>
        /// <returns>An IActionResult.</returns>
        public async Task<DocumentsCheckedOutDTO> CheckOutDocument(int batchItemId)
        {
            DocumentsCheckedOutDTO dtoResult = new();
            await using var trans = _repositoryDocumentsCheckedOut.GetTransaction();
            try
            {
                //creating dto for insert record and response
                dtoResult.BatchItemId = batchItemId;
                dtoResult.SystemUserId = UserId;

                //creating new instance 
                var checkedOut = _mapper.Map<DocumentsCheckedOut>(dtoResult);
                _repositoryDocumentsCheckedOut.Insert(checkedOut);
                _repositoryDocumentsCheckedOut.SaveChanges(UserName,null,null);

                //creating new instance to insert log
                DocumentsCheckedOutLog checkedOutLog = new();
                checkedOutLog.BatchItemId = batchItemId;
                checkedOutLog.SystemUserId = UserId;
                checkedOutLog.CheckedOutAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                checkedOutLog.CheckedInAt = 0;
                _repositoryDocumentsCheckedOutLog.Insert(checkedOutLog);
                _repositoryDocumentsCheckedOutLog.SaveChanges(UserName,null,null);

                //generating response
                dtoResult = _mapper.Map<DocumentsCheckedOutDTO>(checkedOut);

                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
            return dtoResult;
        }

        /// <summary>
        /// Check if the document is already checked out.
        /// </summary>
        /// <param name="batchItemId">The batchItemId representing a document.</param>
        /// <returns>Integer value as DocumentsCheckedOut Id.</returns>
        public int IsDocumentCheckOut(int batchItemId)
        {
            var checkStatus = _repositoryDocumentsCheckedOut.Query(s => s.BatchItemId == batchItemId).FirstOrDefault();
            if (checkStatus != null) return checkStatus.Id;
            else return 0;
        }
    }
}
