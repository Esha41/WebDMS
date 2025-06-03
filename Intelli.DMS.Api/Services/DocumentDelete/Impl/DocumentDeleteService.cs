using AutoMapper;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Api.Services.LocalStorage;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentDelete.Impl
{
    public class DocumentDeleteService : BaseService, IDocumentDeleteService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<BatchItemPage> _repositoryBatchItemPage;
        private readonly IRepository<BatchItemField> _repositoryBatchItemfield;
        private readonly IRepository<BatchMetum> _repositoryBatchItemMeta;
        private readonly IRepository<DocumentApprovalHistory> _repositoryDocumentApprovalHistory;
        private readonly IRepository<DocumentVersion> _repositoryDocumentVersion;
        private readonly IStorageManager _storageManager;
        private readonly string _repositoryRoot;

        public DocumentDeleteService(IMapper mapper,
                                     DMSContext context,
                                     IEventSender sender,
                                     ILogger<DocumentDeleteService> logger,
                                     IConfiguration configuration,
                                     IStorageManager storageManager,
                                     IHttpContextAccessor accessor) : base(accessor)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryRoot = configuration.GetValue<string>("DocumentUploadPath");
            _storageManager = storageManager;
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryBatchItemPage = new GenericRepository<BatchItemPage>(context);
            _repositoryBatchItemfield = new GenericRepository<BatchItemField>(context);
            _repositoryBatchItemMeta = new GenericRepository<BatchMetum>(context);
            _repositoryDocumentApprovalHistory = new GenericRepository<DocumentApprovalHistory>(context);
            _repositoryDocumentVersion = new GenericRepository<DocumentVersion>(context);


            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
               sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
        }

        public async Task DeleteDocumentWithAssociatedData(int batchItemId)
        {
            await using var trans = _repositoryBatchItem.GetTransaction();
            try
            {
                //get batchItem(documentId)
                var batchItem = await _repositoryBatchItem.GetById(batchItemId);

                if (batchItem == null)
                    throw new ArgumentException(MsgKeys.RecordNotExsistsInDatabase);

                //get batch(repository) of batchItem
                var batch = await _repositoryBatch.GetById(batchItem.BatchId);

                //Delete document's details
                _repositoryBatchItemPage.Delete(x => x.BatchItemId == batchItem.Id);
                _repositoryBatchItemPage.SaveChanges(UserName, batch.RequestId, trans);

                //Delete document's fields
                _repositoryBatchItemfield.Delete(x => x.BatchItemId == batchItem.Id);
                _repositoryBatchItemfield.SaveChanges(UserName, batch.RequestId, trans);

                //Delete document's metadata (field values)
                _repositoryBatchItemMeta.Delete(x => x.BatchItemReference == batchItem.BatchItemReference);
                _repositoryBatchItemMeta.SaveChanges(UserName, batch.RequestId, trans);

                //Delete document's approval history
                _repositoryDocumentApprovalHistory.Delete(x => x.BatchItemReference == batchItem.BatchItemReference);
                _repositoryDocumentApprovalHistory.SaveChanges(UserName, batch.RequestId, trans);

                //Delete document's version
                _repositoryDocumentVersion.Delete(x => x.Id == batchItem.DocumentVersionId);
                _repositoryDocumentVersion.SaveChanges(UserName, batch.RequestId, trans);


                //Delete batchItem
                _repositoryBatchItem.Delete(batchItemId, false);
                _repositoryBatchItem.SaveChanges(UserName, batch.RequestId, trans);

                var filePath = RepositoryHelper.BuildFilePath(_repositoryRoot,
                                                                            batch.Id,
                                                                            batchItem.FileName,
                                                                            batch.CreatedDate);

                _storageManager.DeleteFile(filePath);

                await trans.CommitAsync();
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.LogError($"Directory or file not found. \n Message : {ex.Message} \n Error : {ex}");
                await trans.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting document and its associated data. \n Message : {ex.Message} \n Error : {ex}");
                await trans.RollbackAsync();
                throw;
            }
        }
    }
}
