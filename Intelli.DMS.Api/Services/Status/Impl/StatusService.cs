using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.DMS.Api.Services.Status.Impl
{
    public class StatusService : BaseService, IStatusService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<Batch> _repositoryBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/> will be injected</param>
        /// 
        public StatusService(DMSContext context,
           ILogger<StatusService> logger,
           IMapper mapper,
           IEventSender sender,
           IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _mapper = mapper;
            _logger = logger;

            ((GenericRepository<Batch>)_repositoryBatch).AfterSave = (logs) =>
            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };
        }

        /// <summary>
        /// Set BatchItem Status
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <param name="batchItemStatusId"></param>
        /// <returns>  </returns>
        public void SetBatchItemStatus(int batchItemId, int batchItemStatusId, string requestId,
                                           string userName)
        {
            try
            {
                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefault();
                batchItem.BatchItemStatusId = batchItemStatusId;

                _repositoryBatchItem.Update(batchItem);
                _repositoryBatchItem.SaveChanges(userName, requestId, null);

                SetBatchStatusWithBatchItemId(batchItemId, BatchStatusConstants.Created, requestId);
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Set BatchStatus With BatchItemId
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <returns>  </returns>
        public void SetBatchStatusWithBatchItemId(int batchItemId, int batchStatus, string requestId)
        {
            var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefault();
            var batch = _repositoryBatch.Query(x => x.Id == batchItem.BatchId).FirstOrDefault();
            batch.BatchStatusId = batchStatus;
            _repositoryBatch.Update(batch);
            _repositoryBatch.SaveChanges(UserName, requestId, null);
        }

        /// <summary>
        /// Set BatchStatus With ClientId
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <returns>  </returns>
        public void SetBatchStatusWithClientId(int clientId, int batchStatus)
        {
            var batch = _repositoryBatch.Query(x => x.CustomerId == clientId).FirstOrDefault();
            if (batch != null)
            {
                batch.BatchStatusId = batchStatus;
                _repositoryBatch.Update(batch);
                _repositoryBatch.SaveChanges(UserName, null, null);
            }
        }

    }
}
