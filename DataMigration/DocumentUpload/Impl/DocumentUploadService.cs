using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataMigration.DocumentUpload.Impl
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly static int _DEFAULT_ID = 1;
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _repositoryRoot;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<DocumentClass> _repositoryDocumentClass;
        private readonly IRepository<DocumentTypeRoles> _repositoryDocumentTypeRoles;
        private readonly IRepository<DocumentApprovalHistory> _repositoryDocumentApprovalHistory;
        private readonly IRepository<DocumentVersion> _repositoryDocumentVersion;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<BatchItemPage> _repositoryBatchIPage;
        private readonly IRepository<BatchItemField> _repositoryBatchIFields;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<Alert> _repositoryAlert;


        public DocumentUploadService(string documentsSaveToRootPath,string dmsConnectionString)
        {
            DMSContext context = new DMSContext(dmsConnectionString);
            _repositoryRoot = documentsSaveToRootPath;
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryDocumentClass = new GenericRepository<DocumentClass>(context);
            _repositoryDocumentApprovalHistory = new GenericRepository<DocumentApprovalHistory>(context);
            _repositoryDocumentTypeRoles = new GenericRepository<DocumentTypeRoles>(context);
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchIPage = new GenericRepository<BatchItemPage>(context);
            _repositoryBatchIFields = new GenericRepository<BatchItemField>(context);
            _repositoryDocumentVersion = new GenericRepository<DocumentVersion>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);
            _repositoryAlert = new GenericRepository<Alert>(context);
        }

        /// <summary>
        /// Document Upload
        /// </summary>
        /// <param name="file"> Instance of <see cref="IFormFile"/></param>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <returns></returns>
        public async Task DocumentUpload(IFormFile file,
                                          int documentClassId,
                                          int clientId,
                                          int comapnyId,
                                          StringValues httpBatchMetaList,
                                          string UserName,
                                          int UserId,
                                          string localsavePath)
        {
            Batch batch = null;
            await using var trans = _repositoryBatch.GetTransaction();
            try
            {
                // DeserializeObject to BatchMetaDTO list
                var batchMetaList = JsonConvert.DeserializeObject<List<BatchMetaDTO>>(httpBatchMetaList);

                // Save / Find batch
                batch = GetBatch(trans, clientId, comapnyId, UserName).Result;

                // Save / Find DocumentVersion
                var documentVersion = GetDocumentVersion(trans, batch.RequestId, UserName, UserId);

                // Save batch item
                var batchItem = SaveBatchItem(trans,
                                              batch.Id,
                                              documentVersion.Id,
                                              documentClassId,
                                              comapnyId,
                                              batch.RequestId, UserName);

                // Build file path
                var filePath = RepositoryHelper.BuildRepositoryFilePath(_repositoryRoot,
                                                                        batch.Id,
                                                                        batchItem.Id,
                                                                        file.FileName,
                                                                        batch.CreatedDate);

                //Create BatchItemReference
                var batchItemRef = RepositoryHelper.BuildBatchItemReference(batch.Id,
                                                                        batchItem.Id);

                //save Filename and BatchItemReference in Batch Item
                UpdateBatchItemRefInBatchItem(trans,
                                              batchItem.Id,
                                              batchItemRef,
                                              Path.GetFileName(filePath),
                                              batch.RequestId,
                                              UserName);


                // Save batch item page
                SaveBatchItemPage(trans,
                                  batchItem.Id,
                                  documentVersion.Id,
                                  Path.GetFileName(filePath),
                                  file.FileName,
                                  batch.RequestId,
                                  UserName);

                // Save batch item fields
                SaveBatchItemFields(trans,
                                    batchMetaList,
                                    batchItem.Id,
                                    documentVersion.Id,
                                    batch.RequestId,
                                    UserName);

                // Save batch item meta
                SaveBatchItemMeta(trans,
                                  batchMetaList,
                                  batch.Id,
                                  documentVersion.Id,
                                  Path.GetFileName(filePath),
                                  batchItemRef,
                                  batch.RequestId,
                                  UserName);


                //Save Document Approval History
                SaveDocumentApprovalHistory(trans,
                                            documentClassId,
                                            batchItemRef,
                                            out bool DocumentTypeHaveNotApprovalStepRoles,
                                            batch.RequestId, UserName);

                // Send Alert Notification To all Users of 1st Approval Step Role 
                SaveAlertMessageToRole(trans, batchItem.Id, Path.GetFileName(filePath), batch.RequestId, UserName, UserId);

                if (DocumentTypeHaveNotApprovalStepRoles)
                {
                    batchItem.BatchItemStatusId = BatchItemStatusConstants.Approved;
                    _repositoryBatchItem.Update(batchItem);
                    _repositoryBatchItem.SaveChanges(UserName, batch.RequestId, null);
                    batch.BatchStatusId = BatchStatusConstants.Created;
                    _repositoryBatch.Update(batch);
                    _repositoryBatch.SaveChanges(UserName, batch.RequestId, null);
                }
                else
                {
                    batchItem.BatchItemStatusId = BatchItemStatusConstants.Created;
                    _repositoryBatchItem.Update(batchItem);
                    _repositoryBatchItem.SaveChanges(UserName, batch.RequestId, null);
                    batch.BatchStatusId = BatchStatusConstants.Created;
                    _repositoryBatch.Update(batch);
                    _repositoryBatch.SaveChanges(UserName, batch.RequestId, null);
                }

                // Upload document using storage manager service

                var pathOnly = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(pathOnly)) Directory.CreateDirectory(pathOnly);

                using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                
                await trans.CommitAsync();

                if (File.Exists(localsavePath))
                {
                    File.Delete(localsavePath);
                }

                //log Information with request id of batch if exist
                string logMessage = $"Document Upload With Request Id :: {batch.RequestId}";
                Logger.Info(logMessage);

            }
            catch (Exception e)
            {
                //log error with request id of batch if exist
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save Alert Message To that Role who current Review This Document
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemRef"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void SaveAlertMessageToRole(ITransactionHandler transaction,
                                           int batchItemId,
                                           string fileName,
                                           string RequestId,
                                           string UserName,
                                           int UserId)
        {
            try
            {
                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId)
                                                    .Include(x => x.DocumentClass)
                                                    .Include(x => x.Batch)
                                                     .ThenInclude(x => x.Customer)
                                                      .FirstOrDefaultAsync().Result;

                var FirstRole = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItem.BatchItemReference &&
                                                                              x.Approved == false)
                                                                  .FirstOrDefaultAsync().Result;
                if (FirstRole != null)
                {
                    var query = _repositorySystemUserRole.Query(x => x.SystemRoleId == FirstRole.RoleId);

                    var userListAgaistRole = query.ToListAsync().Result;

                    foreach (var user in userListAgaistRole)
                    {
                        if (user.SystemUserId != UserId)
                        {
                            var entity = new Alert();

                            entity.SystemUserId = user.SystemUserId;
                            entity.Msg = $"{batchItem.Batch.Customer.FirstName} " +
                                        $"{batchItem.Batch.Customer.LastName} " +
                                        $"{batchItem.DocumentClass.DocumentClassName} " +
                                        $"is available for review, please proceed. " +
                                        $"Document name is {fileName}.";
                            _repositoryAlert.Insert(entity, true);
                        }
                    }

                    _repositoryAlert.SaveChanges(UserName, RequestId, transaction);
                }
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save Document Approval History 
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentClassId"></param>
        /// <param name="batchItemRef"></param>
        /// <returns></returns>
        public void SaveDocumentApprovalHistory(ITransactionHandler trans,
                                                int documentClassId,
                                                string batchItemRef,
                                                out bool documentTypeHaveNotApprovalStepRoles,
                                                string RequestId,
                                           string UserName)
        {
            try
            {
                var documentTypeId = _repositoryDocumentClass.Query(x => x.Id == documentClassId)
                                                              .FirstOrDefaultAsync().Result.DocumentTypeId;

                var documentTypeRoleList = _repositoryDocumentTypeRoles.Query(x => x.DocumentTypeId == documentTypeId)
                                                                        .ToListAsync().Result;
                if (documentTypeRoleList.Count > 0)
                {
                    foreach (var item in documentTypeRoleList)
                    {
                        DocumentApprovalHistory documentApprovalHistory = new()
                        {
                            BatchItemReference = batchItemRef,
                            RoleId = item.SystemRoleId,
                            Approved = false
                        };
                        _repositoryDocumentApprovalHistory.Insert(documentApprovalHistory);
                    }
                    _repositoryDocumentApprovalHistory.SaveChanges(UserName, RequestId, trans);
                    documentTypeHaveNotApprovalStepRoles = false;
                }
                else
                {
                    documentTypeHaveNotApprovalStepRoles = true;
                }
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }



        /// <summary>
        /// Save FileName In BatchItem
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="transaction"></param>
        /// <param name="batchItemId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void UpdateBatchItemRefInBatchItem(ITransactionHandler transaction,
                                             int batchItemId,
                                             string BatchItemRef,
                                             string fileName,
                                             string RequestId,
                                           string UserName)
        {
            try
            {
                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefaultAsync().Result;
                batchItem.BatchItemReference = BatchItemRef;
                batchItem.FileName = fileName;
                _repositoryBatchItem.Update(batchItem);
                _repositoryBatchItem.SaveChanges(UserName, RequestId, transaction);
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }

        }

        /// <summary>
        /// Save Document Version
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <returns>DocumentVersion</returns>
        public DocumentVersion GetDocumentVersion(ITransactionHandler trans, string requestId,
                                           string UserName, int UserId)
        {
            try
            {
                // If not create new batch and return
                var documentVersion = new DocumentVersion
                {
                    Version = 1,
                    Comments = "",
                    LastModifiedBy = UserId.ToString()
                };

                _repositoryDocumentVersion.Insert(documentVersion);
                _repositoryDocumentVersion.SaveChanges(UserName, requestId, trans);
                return documentVersion;
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save batch
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="clientId"></param>
        /// <param name="companyId"></param>
        /// <returns>Task of Batch </returns>
        public async Task<Batch> GetBatch(ITransactionHandler trans,
                                           int clientId,
                                           int companyId,
                                           string UserName)
        {
            try
            {
                // If existing batch of client exists return the batch
                var query = _repositoryBatch.Query(x => x.CustomerId == clientId);
                var batch = await query.FirstOrDefaultAsync();

                // If found return batch
                if (batch != null) return batch;

                // If not create new batch and return
                var entityBatch = new Batch
                {
                    CreatedDate = DateTime.Now.Date,
                    RequestId = Guid.NewGuid().ToString(),
                    BatchSourceId = _DEFAULT_ID,
                    BatchStatusId = _DEFAULT_ID,
                    MandatoryAlerts = null,
                    ValidationAlerts = null,
                    CustomerId = clientId,
                    CompanyId = companyId
                };

                _repositoryBatch.Insert(entityBatch);
                _repositoryBatch.SaveChanges(UserName, entityBatch.RequestId, trans);

                return entityBatch;
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save batch item
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchId"></param>
        /// <param name="documentClassId"></param>
        /// <returns>BatchItem</returns>
        public BatchItem SaveBatchItem(ITransactionHandler transaction,
                                        int batchId,
                                        int documentVersionId,
                                        int documentClassId,
                                        int companyId,
                                        string RequestId,
                                           string UserName)
        {
            try
            {
                var batchItem = new BatchItem
                {
                    BatchId = batchId,
                    DocumentVersionId = documentVersionId,
                    BatchItemStatusId = _DEFAULT_ID,
                    DocumentClassId = documentClassId,
                    OccuredAt = DateTime.Now,
                    ParentId = null,
                    CompanyId = companyId
                };

                _repositoryBatchItem.Insert(batchItem);
                _repositoryBatchItem.SaveChanges(UserName, RequestId, transaction);

                return batchItem;
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }


        /// <summary>
        /// Save batch item page
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <param name="fileName"></param>
        /// <param name="pageNo"></param>
        public void SaveBatchItemPage(ITransactionHandler transaction,
                                       int batchItemId,
                                       int documentVersionId,
                                       string fileName,
                                       string originalFileName,
                                       string RequestId,
                                           string UserName,
                                       int pageNo = 1)
        {
            try
            {
                var batchItemPage = new BatchItemPage
                {
                    BatchItemId = batchItemId,
                    DocumentVersionId = documentVersionId,
                    FileName = fileName,
                    Number = pageNo,
                    OriginalName = originalFileName
                };

                _repositoryBatchIPage.Insert(batchItemPage);
                _repositoryBatchIPage.SaveChanges(UserName, RequestId, transaction);
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save batch item fields
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchMetaList"></param>
        /// <param name="batchItemId"></param>
        public void SaveBatchItemFields(ITransactionHandler transaction,
                                         List<BatchMetaDTO> batchMetaList,
                                         int batchItemId,
                                         int documentVersionId,
                                         string RequestId,
                                           string UserName)
        {
            try
            {
                foreach (BatchMetaDTO item in batchMetaList)
                {
                    var batchItemFields = new BatchItemField
                    {
                        BatchItemId = batchItemId,
                        DocumentVersionId = documentVersionId,
                        DictionaryValueId = item.DictionaryValueId,
                        DocumentClassFieldId = item.DocumentClassFieldId,
                        RegisteredFieldValue = item.FieldValue,
                        DictionaryValueIdOld = null,
                        RegisteredFieldValueOld = null,
                        IsLast = true,
                    };

                    _repositoryBatchIFields.Insert(batchItemFields);
                    _repositoryBatchIFields.SaveChanges(UserName, RequestId, transaction);
                }
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }

        /// <summary>
        /// Save batch item meta
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchMetaList"></param>
        /// <param name="batchId"></param>
        public void SaveBatchItemMeta(ITransactionHandler transaction,
                                       List<BatchMetaDTO> batchMetaList,
                                       int batchId,
                                       int documentVersionId,
                                       string fileName,
                                       string batchItemRef,
                                       string RequestId,
                                       string UserName)
        {
            try
            {
                foreach (BatchMetaDTO item in batchMetaList)
                {
                    var entityMeta = new BatchMetum
                    {
                        BatchId = batchId,
                        FileName = fileName,
                        BatchItemReference = batchItemRef,
                        DocumentVersionId = documentVersionId,
                        DictionaryValueId = item.DictionaryValueId,
                        DocumentClassFieldId = item.DocumentClassFieldId,
                        FieldValue = item.FieldValue,
                    };

                    _repositoryBatchMeta.Insert(entityMeta);
                    _repositoryBatchMeta.SaveChanges(UserName, RequestId, transaction);
                }
            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}: {e}";
                Logger.Error(logMessage);
                throw;
            }
        }
    }
}
