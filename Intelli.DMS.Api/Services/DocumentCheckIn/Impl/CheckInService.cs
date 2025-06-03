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

namespace Intelli.DMS.Api.Services.DocumentCheckIn.Impl
{
    public class CheckInService: BaseService, ICheckInService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;
        private readonly IRepository<DocumentsCheckedOutLog> _repositoryDocumentsCheckedOutLog;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;
        private readonly IRepository<BatchItemField> _repositoryBatchIFields;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckOutService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        /// 
        public CheckInService(DMSContext context,
           ILogger<CheckInService> logger,
           IEventSender sender, IMapper mapper,
           IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _repositoryDocumentsCheckedOutLog = new GenericRepository<DocumentsCheckedOutLog>(context);
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);
            _repositoryBatchIFields = new GenericRepository<BatchItemField>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);

            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave =
            ((GenericRepository<BatchItemField>)_repositoryBatchIFields).AfterSave =
            ((GenericRepository<DocumentsCheckedOut>)_repositoryDocumentsCheckedOut).AfterSave =
            ((GenericRepository<DocumentsCheckedOutLog>)_repositoryDocumentsCheckedOutLog).AfterSave =
            ((GenericRepository<BatchMetum>)_repositoryBatchMeta).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Checked in a document.
        /// </summary>
        /// <param name="batchItemId">batchItemId relevant to document.</param>
        /// <param name="docCheckOutId">docCheckOutId to be deleted.</param>
        /// <returns>boolean if document is checked In.</returns>
        public async Task<bool> CheckInDocument(int batchItemId, int docCheckOutId)
        {
            await using var trans = _repositoryDocumentsCheckedOut.GetTransaction();
            try
            {
                // deleting the existing record from db if doc gets check in 
                _repositoryDocumentsCheckedOut.Delete(docCheckOutId, false);
                _repositoryDocumentsCheckedOut.SaveChanges(UserName,null,null);

                //Update log
                DocumentsCheckedOutLog checkedInLog = _repositoryDocumentsCheckedOutLog.Query(x => x.BatchItemId == batchItemId)
                                                                                     .FirstOrDefaultAsync()
                                                                                      .Result;
                if (checkedInLog != null)
                {
                    checkedInLog.SystemUserId = UserId;
                    checkedInLog.CheckedInAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    _repositoryDocumentsCheckedOutLog.Update(checkedInLog);
                    _repositoryDocumentsCheckedOutLog.SaveChanges(UserName,null,null);
                }

                await trans.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Check if document check in is allowed by the current user.
        /// </summary>
        /// <param name="batchItemId">The batchItemId representing a document.</param>
        /// <param name="UserId">The current userId.</param>
        /// <returns>Integer value as DocumentsCheckedOut Id.</returns>
        public int IsCheckInAllowed(int batchItemId, int userId)
        {
            var docCheckOut = _repositoryDocumentsCheckedOut.Query(s => s.BatchItemId == batchItemId && s.SystemUserId == UserId).FirstOrDefault();

            //return 0 if checkedIn is not allowed otherwise an Id of DocumentsCheckedOut
            if (docCheckOut != null) return docCheckOut.Id;
            else return 0;
        }

    }
}
