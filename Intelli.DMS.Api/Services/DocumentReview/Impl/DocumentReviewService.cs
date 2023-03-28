using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.DMS.Api.Services.DocumentReview.Impl
{
    public class DocumentReviewService : BaseService, IDocumentReviewService
    {
        private readonly ILogger _logger;
        private readonly IRepository<BatchItemPage> _repositoryBatchItemPages;
        private readonly IRepository<DocumentsCheckedOutLog> _repositoryDocumentsCheckedOutLog;
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentReviewService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        /// 
        public DocumentReviewService(DMSContext context,
           ILogger<DocumentReviewService> logger,
           IEventSender sender,
           IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryDocumentsCheckedOutLog = new GenericRepository<DocumentsCheckedOutLog>(context);
            _repositoryBatchItemPages = new GenericRepository<BatchItemPage>(context);
            _logger = logger;

            ((GenericRepository<DocumentsCheckedOutLog>)_repositoryDocumentsCheckedOutLog).AfterSave =
            ((GenericRepository<BatchItemPage>)_repositoryBatchItemPages).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };
        }

        // <summary>
        /// Get All documents of pending review and which are not checked out yet.
        /// </summary>
        /// <returns>List of documents to be reviews</returns>
        public List<DocumentReviewDTO> GetPendingReviewDoc()
        {
            try
            {
                //getting list of documents except "reviewed documents" 
                var batchItemList = _repositoryBatchItemPages.Query(d => d.BatchItem.BatchItemStatus.BatchItemStatusName != "Reviewed Ok")
                                                             .Include(a => a.BatchItem)
                                                             .ThenInclude(a => a.Batch)
                                                             .ThenInclude(a => a.Customer)
                                                             .ToList();

                //checkout documents list of current login user 
                var getUserDoc = _repositoryDocumentsCheckedOutLog.Query(s => s.SystemUserId == UserId).Select(s=>s.BatchItemId).ToList();

                //removing that list from documents of login user
                batchItemList.RemoveAll(s => getUserDoc.Contains(s.BatchItemId));

                //setting response
               List<DocumentReviewDTO> docReviewList = new();
               foreach(var item in batchItemList)
                {
                    //creating object of DocumentReviewDTO and adding to list
                    var getCustomer = item.BatchItem.Batch.Customer;
                    DocumentReviewDTO docReviewObject = new()
                    {
                        Id = item.Id,
                        FileName = item.FileName,
                        ClientName = getCustomer.FirstName + " " + getCustomer.LastName,
                        IsActive = item.IsActive,
                        UpdatedAt = item.UpdatedAt,
                        CreatedAt = item.CreatedAt,
                        Version = 0,
                        State = 0,
                        Status = null
                    };

                    docReviewList.Add(docReviewObject);
                }

                //todo: double check

                return docReviewList;
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }
    }
}
