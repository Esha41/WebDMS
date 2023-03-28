using AutoMapper;
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

namespace Intelli.DMS.Api.Services.DocumentFields.Impl
{
    public class DocumentFieldService:BaseService, IDocumentFieldService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;
        private readonly IRepository<BatchItemField> _repositoryBatchIFields;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<BatchItemStatus> _repositoryBatchItemStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFieldService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        
        public DocumentFieldService(DMSContext context,
           ILogger<DocumentFieldService> logger,
           IEventSender sender, IMapper mapper,
           IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);
            _repositoryBatchIFields = new GenericRepository<BatchItemField>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchItemStatus = new GenericRepository<BatchItemStatus>(context);

            ((GenericRepository<BatchItemStatus>)_repositoryBatchItemStatus).AfterSave = (logs) =>
            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
            ((GenericRepository<BatchItemField>)_repositoryBatchIFields).AfterSave = (logs) =>
            ((GenericRepository<BatchMetum>)_repositoryBatchMeta).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get PrevField Values By BatchId
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public List<UpdatedFieldValuesDTO> GetPrevFieldValues(int batchId)
        {
            try
            {
                var metaList = _repositoryBatchMeta.Query(filter: s => s.BatchId == batchId)
                        .Include(d => d.DocumentClassField).ToList();
                var result = _mapper.Map<List<UpdatedFieldValuesDTO>>(metaList);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// performing changes to document i.e changes in its document class field values
        /// </summary>
        /// <param name="List<BatchMetaCheckOutDTO>"></param>
        public async Task<List<BatchMetaCheckOutDTO>> UpdateFieldValues(List<BatchMetaCheckOutDTO> dtoList)
        {
            await using var trans = _repositoryBatchMeta.GetTransaction();
            try
            {
                //retrieving batchItem Id from batchId
                var batchItem = _repositoryBatchItem.Query(s => dtoList.Select(d => d.BatchId).Contains(s.BatchId)).FirstOrDefault();

                // Updating batch item meta
                UpdateBatchItemMeta(trans, dtoList);

                // Updating batch item fields
                UpdateBatchItemFields(trans, dtoList, batchItem.Id);

                // Updating batch item status
                UpdateBatchItem(trans, batchItem.Id);

                //generating response
                var response = _mapper.Map<List<BatchMetaCheckOutDTO>>(dtoList);

                await trans.CommitAsync();

                return response;
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
        /// update batch meta
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="batchMetaList"></param>
        private void UpdateBatchItemMeta(ITransactionHandler transaction,
                                       List<BatchMetaCheckOutDTO> batchMetaList)
        {
            foreach (BatchMetaCheckOutDTO item in batchMetaList)
            {
                BatchMetum batchMeta = _repositoryBatchMeta.Query(s => s.Id == item.Id).FirstOrDefault();
                batchMeta.FieldValue = item.FieldValue;
                batchMeta.DictionaryValueId = item.DictionaryValueId;
                _repositoryBatchMeta.Update(batchMeta);
            }
            _repositoryBatchMeta.SaveChanges(UserName,null,transaction);
        }

        /// <summary>
        /// update batch item fields
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="batchMetaList"></param>
        private void UpdateBatchItemFields(ITransactionHandler transaction,
                                       List<BatchMetaCheckOutDTO> batchMetaList
                                       , int batchItemId)
        {
            foreach (BatchMetaCheckOutDTO item in batchMetaList)
            {
                var batchIFields = _repositoryBatchIFields.Query(s => s.DocumentClassFieldId == item.DocumentClassFieldId && s.BatchItemId == batchItemId).FirstOrDefault();
                batchIFields.RegisteredFieldValueOld = batchIFields.RegisteredFieldValue;
                batchIFields.RegisteredFieldValue = item.FieldValue;
                batchIFields.DictionaryValueIdOld = batchIFields.DictionaryValueId;
                batchIFields.DictionaryValueId = item.DictionaryValueId;

                _repositoryBatchIFields.Update(batchIFields);
            }

            _repositoryBatchIFields.SaveChanges(UserName,null,transaction);
        }

        /// <summary>
        /// update batch item fields
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="batchMetaList"></param>
        private void UpdateBatchItem(ITransactionHandler transaction, int batchItemId)
        {
            //getting pending Review id from bacthItemStatus
            var getStatusId = _repositoryBatchItemStatus.Query(d => d.EnumValue == "Pending Review").FirstOrDefault().Id;

            //set "pending review" status for the updated document (in batchItems table)
            var batchItem = _repositoryBatchItem.Query(s => s.Id == batchItemId).FirstOrDefault();
            batchItem.BatchItemStatusId = getStatusId;
            _repositoryBatchItem.Update(batchItem);
            _repositoryBatchItem.SaveChanges(UserName,null,transaction);
        }
    }
}
