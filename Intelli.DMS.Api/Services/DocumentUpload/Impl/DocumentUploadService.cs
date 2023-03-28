using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Api.Services.DocumentApproved;
using Intelli.DMS.Api.Services.LocalStorage;
using Intelli.DMS.Api.Services.Status;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Resources;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentUpload.Impl
{
    public class DocumentUploadService : BaseService, IDocumentUploadService
    {
        private readonly static int _DEFAULT_ID = 1;

        private readonly string _repositoryRoot;
        private readonly IStorageManager _storageManager;
        private readonly IStatusService _statusService;
        private readonly IDocumentApprovedService _documentApprovedService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<DocumentClass> _repositoryDocumentClass;
        private readonly IRepository<DocumentClassField> _repositoryDocumentClassField;
        private readonly IRepository<DocumentTypeRoles> _repositoryDocumentTypeRoles;
        private readonly IRepository<DocumentApprovalHistory> _repositoryDocumentApprovalHistory;
        private readonly IRepository<DocumentVersion> _repositoryDocumentVersion;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<BatchItemPage> _repositoryBatchIPage;
        private readonly IRepository<BatchItemField> _repositoryBatchIFields;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;
        private readonly IRepository<DocumentsCheckedOutLog> _repositoryDocumentsCheckedOutLog;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<SystemUser> _repositorySystemUser;
        private readonly IRepository<Alert> _repositoryAlert;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUploadController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="storageManager">Instance of <see cref="IStorageManager"/> will be injected</param>
        /// <param name="statusService">Instance of <see cref="IStatusService"/> will be injected</param>
        /// <param name="documentApprovedService">Instance of <see cref="IDocumentApprovedService"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/> will be injected</param>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/> will be injected</param>
        public DocumentUploadService(DMSContext context, IStorageManager storageManager,
                                        IStatusService statusService,
                                        IDocumentApprovedService documentApprovedService,
                                        IMapper mapper,
                                        ILogger<DocumentUploadService> logger,
                                        IConfiguration configuration,
                                        IEventSender sender,
                                        IHttpContextAccessor accessor) : base(accessor)
        {
            _storageManager = storageManager;
            _documentApprovedService = documentApprovedService;
            _statusService = statusService;
            _logger = logger;
            _configuration = configuration;
            _repositoryRoot = _configuration.GetSection("DocumentUploadPath").Value;
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryDocumentClass = new GenericRepository<DocumentClass>(context);
            _repositoryDocumentClassField = new GenericRepository<DocumentClassField>(context);
            _repositoryDocumentApprovalHistory = new GenericRepository<DocumentApprovalHistory>(context);
            _repositoryDocumentTypeRoles = new GenericRepository<DocumentTypeRoles>(context);
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchIPage = new GenericRepository<BatchItemPage>(context);
            _repositoryBatchIFields = new GenericRepository<BatchItemField>(context);
            _repositoryDocumentVersion = new GenericRepository<DocumentVersion>(context);
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _repositoryDocumentsCheckedOutLog = new GenericRepository<DocumentsCheckedOutLog>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);
            _repositoryAlert = new GenericRepository<Alert>(context);
            _repositorySystemUser = new GenericRepository<SystemUser>(context);
            _mapper = mapper;


            ((GenericRepository<DocumentClassField>)_repositoryDocumentClassField).AfterSave =
            ((GenericRepository<SystemUser>)_repositorySystemUser).AfterSave =
            ((GenericRepository<Alert>)_repositoryAlert).AfterSave =
            ((GenericRepository<SystemUserRole>)_repositorySystemUserRole).AfterSave =
            ((GenericRepository<DocumentsCheckedOutLog>)_repositoryDocumentsCheckedOutLog).AfterSave =
            ((GenericRepository<DocumentsCheckedOut>)_repositoryDocumentsCheckedOut).AfterSave =
            ((GenericRepository<DocumentVersion>)_repositoryDocumentVersion).AfterSave =
            ((GenericRepository<BatchItemField>)_repositoryBatchIFields).AfterSave =
            ((GenericRepository<BatchItemPage>)_repositoryBatchIPage).AfterSave =
            ((GenericRepository<Batch>)_repositoryBatch).AfterSave =
            ((GenericRepository<BatchMetum>)_repositoryBatchMeta).AfterSave =
            ((GenericRepository<DocumentTypeRoles>)_repositoryDocumentTypeRoles).AfterSave =
            ((GenericRepository<DocumentApprovalHistory>)_repositoryDocumentApprovalHistory).AfterSave =
            ((GenericRepository<DocumentClass>)_repositoryDocumentClass).AfterSave =
             ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };
        }

        /// <summary>
        /// Upload Contract
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Task </returns>
        public async Task UploadContract(UploadContractDTO dto,
                                           string UserName,
                                          int UserId)
        {
            Batch batch = null;
            int count = 0;

            await using var trans = _repositoryBatch.GetTransaction();
            try
            {
                // Save / Find batch
                batch = GetBatch(trans, dto.ClientInfo.Id, dto.ClientInfo.CompanyId, UserName).Result;

                // Save / Find DocumentVersion
                var documentVersion = GetDocumentVersion(trans, batch.RequestId, UserName, UserId);

                //get document class field Ids by doc class field codes
                var docClassFieldCodes = dto.DocumentClasses.SelectMany(x => x.DocumentClassFields).Select(c => c.DocumentClassFieldCode).ToList();
                var allDocClassFields = _repositoryDocumentClassField.Query(x => docClassFieldCodes.Contains(x.DocumentClassFieldCode))
                                    .ToList();
                dto.DocumentClasses.ForEach(d => d.DocumentClassFields.ForEach(x => x.Id = allDocClassFields.FirstOrDefault(c => c.DocumentClassFieldCode == x.DocumentClassFieldCode)?.Id ?? 0));

                //get document class Ids by doc class codes
                var docClassCodes = dto.DocumentClasses.Select(x => x.DocumentClassCode).ToList();
                var allDocClass = _repositoryDocumentClass.Query(x => docClassCodes.Contains(x.DocumentClassCode))
                                    .ToList();
                if(allDocClass.Count == 0)
                {
                    throw new Exception(MsgKeys.DocumentclassNotExist);
                }
                dto.DocumentClasses.ForEach(x => x.Id = allDocClass.FirstOrDefault(c => c.DocumentClassCode == x.DocumentClassCode)?.Id ?? 0);

                foreach (var documentClass in dto.DocumentClasses.Where(x => x.Id != 0))
                {
                    IFormFile file ;
                    count = count + 1;
                    string fileType = "";
                    documentClass.BatchMetaList = _mapper.Map<List<BatchMetaDTO>>(documentClass.DocumentClassFields.Where(x => x.Id != 0));
                    if (documentClass.Images.Count > 1)
                    {
                         fileType = "pdf";
                    }
                    else
                    {
                        byte[] bytes = Convert.FromBase64String(documentClass.Images[0]);
                        fileType = GetFileType(bytes);
                    }


                    // Save batch item
                    var batchItem = SaveBatchItem(trans,
                                                          batch.Id,
                                                          documentVersion.Id,
                                                          documentClass.Id,
                                                          dto.ClientInfo.CompanyId,
                                                          batch.RequestId, UserName);

                        // Build file path
                        var filePath = RepositoryHelper.BuildRepositoryFilePathForMultipleFiles(_repositoryRoot,
                                                                                    batch.Id,
                                                                                    batchItem.Id,
                                                                                    UserId.ToString(),
                                                                                    fileType,
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

                        // Save batch item fields 
                        SaveBatchItemFields(trans,
                                            documentClass.BatchMetaList,
                                            batchItem.Id,
                                            documentVersion.Id,
                                            batch.RequestId,
                                            UserName);

                        // Save batch item page
                        SaveBatchItemPage(trans,
                                          batchItem.Id,
                                          documentVersion.Id,
                                          Path.GetFileName(filePath),
                                          UserId.ToString() + "." + fileType,
                                          batch.RequestId,
                                          UserName);

                        // Save batch item meta
                        SaveBatchItemMeta(trans,
                                          documentClass.BatchMetaList,
                                          batch.Id,
                                          documentVersion.Id,
                                          Path.GetFileName(filePath),
                                          batchItemRef,
                                          batch.RequestId,
                                          UserName);

                    //Save Document Approval History
                    //SaveDocumentApprovalHistory(trans,
                    //                            documentClass.Id,
                    //                            batchItemRef,
                    //                            out bool DocumentTypeHaveNotApprovalStepRoles,
                    //                            batch.RequestId, UserName);

                    //// Send Alert Notification To all Users of 1st Approval Step Role 
                    //SaveAlertMessageToRole(trans, batchItem.Id, Path.GetFileName(filePath), batch.RequestId, UserName, UserId);

                    //if (DocumentTypeHaveNotApprovalStepRoles)
                    //{
                    //    _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Approved, batch.RequestId, UserName);
                    //}
                    //else
                    //{
                    //    _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Created, batch.RequestId, UserName);
                    //}

                    // Upload document using storage manager service
                    if (documentClass.Images.Count > 1)
                    {
                         CombineImagesToPdf(documentClass.Images,filePath);
                    }
                    else
                    {
                        byte[] bytes = Convert.FromBase64String(documentClass.Images[0]);

                        fileType = GetFileType(bytes);

                        MemoryStream stream = new MemoryStream(bytes);

                        file = new FormFile(stream, 0, bytes.Length, "file", UserId.ToString() + "." + fileType);
                        await _storageManager.StoreFile(file, filePath).ConfigureAwait(true);

                    }
                    count = 0;
                }
                await trans.CommitAsync();

                //log Information with request id of batch if exist
                string logMessage = $"Document Upload With Request Id :: {batch.RequestId}";
                NLogCustomPropertyHelper.LogInformationWithRequestId(_logger, batch?.RequestId, logMessage);
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                //log error with request id of batch if exist
                string logMessage = $"{e.Message}: {e}";
                NLogCustomPropertyHelper.LogErrorWithRequestId(_logger, batch?.RequestId, logMessage);
                throw;
            }
        }
        private void CombineImagesToPdf(List<string>images,string filepath)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filepath, FileMode.Create));
            doc.Open();
            foreach(var img in images)
            {
                byte[] bytes = Convert.FromBase64String(img);
                System.Drawing.Image image;
                var tempPath = "";
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = System.Drawing.Image.FromStream(ms);
                    tempPath = Path.GetTempFileName();
                    image.Save(tempPath);
                }
                Image docImage = Image.GetInstance(tempPath);
                //scale the images to fit the document size
                docImage.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                docImage.Alignment = Image.ALIGN_CENTER; //align the pictures
                doc.Add(docImage);
                image.Dispose();

            }
            doc.Dispose();
        }
        private string GetFileType(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            using (var br = new BinaryReader(ms))
            {
                var signatures = new Dictionary<string, string>
            {
                {"ffd8ffe0", "jpg"},
                {"89504e47", "png"},
                {"47494638", "gif"},
                {"49492a00", "tif"},
                {"424d", "bmp"},
                {"25504446", "pdf"},
                // add more file signatures here
            };

                var byteSignature = BitConverter.ToString(br.ReadBytes(4)).ToLower();
                var signature = byteSignature.Replace("-", "");

                foreach (var sig in signatures)
                {
                    if (signature.StartsWith(sig.Key))
                    {
                        return sig.Value;
                    }
                }

                return "unknown";
            }
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
                                          int UserId)
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
                    _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Approved, batch.RequestId, UserName);
                }
                else
                {
                    _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Created, batch.RequestId, UserName);
                }

                // Upload document using storage manager service
                await _storageManager.StoreFile(file, filePath).ConfigureAwait(true);

                await trans.CommitAsync();

                //log Information with request id of batch if exist
                string logMessage = $"Document Upload With Request Id :: {batch.RequestId}";
                NLogCustomPropertyHelper.LogInformationWithRequestId(_logger, batch?.RequestId, logMessage);

            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                //log error with request id of batch if exist
                string logMessage = $"{e.Message}: {e}";
                NLogCustomPropertyHelper.LogErrorWithRequestId(_logger, batch?.RequestId, logMessage);
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

                var firstRole = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItem.BatchItemReference &&
                                                                              x.Approved == false)
                                                                  .FirstOrDefaultAsync().Result;
                if (firstRole != null)
                {
                    var query = _repositorySystemUserRole.Query(x => x.SystemRoleId == firstRole.RoleId);

                    var userListAgaistRole = query.ToListAsync().Result;

                    AlertDTO dto = new();

                    foreach (var user in userListAgaistRole)
                    {
                        if (user.SystemUserId != UserId)
                        {
                            var entity = _mapper.Map<Alert>(dto);

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

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Update Alert Message To Users of that Role who Currently Review this Document
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="transaction"></param>
        /// <param name="batchItemRef"></param>
        /// <param name="currentFileName"></param>
        /// <param name="perviousFileName"></param>
        /// <returns></returns>
        public void UpdateAlertMessageToRole(ITransactionHandler transaction,
                                             int batchItemId,
                                             string currentFileName,
                                             string perviousFileName,
                                             string RequestId,
                                             string userName,
                                             int userId)
        {
            try
            {
                var perviousBatchItem = _repositoryBatchItem.Query(x => x.FileName == perviousFileName)
                                                  .Include(x => x.DocumentClass)
                                                  .Include(x => x.Batch)
                                                   .ThenInclude(x => x.Customer)
                                                    .FirstOrDefaultAsync().Result;

                _repositoryAlert.Delete(x => x.Msg == $"{perviousBatchItem.Batch.Customer.FirstName} " +
                                                      $"{perviousBatchItem.Batch.Customer.LastName} " +
                                                      $"{perviousBatchItem.DocumentClass.DocumentClassName} " +
                                                      $"is available for review, please proceed. " +
                                                      $"Document name is {currentFileName}.");
                _repositoryAlert.SaveChanges(userName, RequestId, transaction);

                SaveAlertMessageToRole(transaction, batchItemId, currentFileName, RequestId, userName, userId);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                                string requestId,
                                           string userName)
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
                    _repositoryDocumentApprovalHistory.SaveChanges(userName, requestId, trans);
                    documentTypeHaveNotApprovalStepRoles = false;
                }
                else
                {
                    documentTypeHaveNotApprovalStepRoles = true;
                }
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                             string requestId,
                                           string userName)
        {
            try
            {
                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefaultAsync().Result;
                batchItem.BatchItemReference = BatchItemRef;
                batchItem.FileName = fileName;
                _repositoryBatchItem.Update(batchItem);
                _repositoryBatchItem.SaveChanges(userName, requestId, transaction);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }

        }

        /// <summary>
        /// Document Edit 
        /// </summary>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="editUploadedDocumentDTO"></param>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <returns></returns>
        public async Task DocumentEdit(int documentClassId,
                                       int clientId,
                                       EditUploadedDocumentDTO editUploadedDocumentDTO,
                                           string userName, int userId)
        {
            Batch batch = null;

            await using var trans = _repositoryBatch.GetTransaction();
            try
            {
                // DeserializeObject to BatchMetaDTO list
                var batchMetaList = JsonConvert.DeserializeObject<List<BatchMetaDTO>>(editUploadedDocumentDTO.BatchMetaData);

                //Get Previous BatchItem With Document Name by Converting In Membership Clone 
                var perviousBatchItem = (BatchItem)GetBatchByFileName(trans, editUploadedDocumentDTO.DocumentName).Clone();


                DateTime perviousBatchItemBatchCreatedDate = perviousBatchItem.Batch.CreatedDate;

                // Save / Find batch
                batch = GetBatch(trans, clientId, editUploadedDocumentDTO.CompanyId, userName).Result;

                //  Find Previous DocumentVersion
                var previousDocumentVersion = GetPreviousDocumentVersion(perviousBatchItem.DocumentVersionId);


                // Save /Find Incremented DocumentVersion
                var documentVersion = GetIncrementDocumentVersion(trans,
                                                                 batch.Id,
                                                                 previousDocumentVersion.Version,
                                                                 editUploadedDocumentDTO.Comment,
                                                                 batch.RequestId,
                                                                 userName,
                                                                 userId);

                // Save batch item and update BatchId
                var batchItem = UpdateBatchItem(trans,
                                               batch.Id,
                                               documentVersion.Id,
                                               documentClassId,
                                               perviousBatchItem.BatchItemReference,
                                               perviousBatchItem.Id,
                                               perviousBatchItem.BatchItemStatusId,
                                               perviousBatchItem.SystemRoleId,
                                               editUploadedDocumentDTO.DocumentName,
                                               editUploadedDocumentDTO.CompanyId,
                                               editUploadedDocumentDTO.IsApproved,
                                               batch.RequestId,
                                               userName);

                //Build Current BatchItemReference 
                var currentBatchItemRef = RepositoryHelper.BuildBatchItemReference(batch.Id,
                                                                        batchItem.Id);


                if (!editUploadedDocumentDTO.IsRejected)
                {
                    // Save batch item fields
                    SaveBatchItemFields(trans,
                                        batchMetaList,
                                        batchItem.Id,
                                        documentVersion.Id,
                                        batch.RequestId,
                                        userName);


                    // Save batch item meta
                    SaveBatchItemMeta(trans,
                                          batchMetaList,
                                          batch.Id,
                                          documentVersion.Id,
                                          editUploadedDocumentDTO.DocumentName,
                                          currentBatchItemRef,
                                          batch.RequestId,
                                          userName);
                }
                //Update Checkout Document Version
                UpdateCheckoutDcouments(trans,
                                        editUploadedDocumentDTO.DocumentId,
                                        batchItem.Id,
                                        batch.RequestId,
                                        userName,
                                        userId);

                //Build FileName Of Previous Document Version
                var fileNamePervious = RepositoryHelper.BuildFileName(perviousBatchItem.BatchId,
                                                                  perviousBatchItem.Id,
                                                                  perviousBatchItem.FileName);
                //Build FileName Of Current Document Version
                var fileNameCurrent = RepositoryHelper.BuildFileName(batch.Id,
                                                                        batchItem.Id,
                                                                        batchItem.FileName);

                // Update FileName And BatchItemRef In BatchItem And BatchItemRef
                // In BatchItemPage
                // DocumentApprovalHistory Entities
                Update_FileName_And_BatchItemRef_In_BatchItem_And_BatchItemRef_InBatchItemPage_DocumentApprovalHistory_Entities(trans,
                                                                                    perviousBatchItem.BatchItemReference,
                                                                                    currentBatchItemRef,
                                                                                    fileNameCurrent,
                                                                                    batchItem.Id,
                                                                                    batch.RequestId,
                                                                                    userName);



                // Build file path of Previous Document Version
                var filePathPervious = RepositoryHelper.BuildRepositoryFilePath(_repositoryRoot,
                                                                         perviousBatchItem.BatchId,
                                                                         perviousBatchItem.Id,
                                                                         perviousBatchItem.FileName,
                                                                         perviousBatchItemBatchCreatedDate);
                // Build file path of Current Document Version
                var filePathCurrent = RepositoryHelper.BuildRepositoryFilePath(_repositoryRoot,
                                                                         batch.Id,
                                                                         batchItem.Id,
                                                                         batchItem.FileName,
                                                                         batch.CreatedDate);
                // Check Previous Document Version Path Exists
                if (File.Exists(filePathPervious))
                {

                    //Get DirectoryName of Current Document Version
                    var pathOnly = Path.GetDirectoryName(filePathCurrent);

                    //If Current Document Version Directory does Not Exists Create It 
                    if (!Directory.Exists(pathOnly)) Directory.CreateDirectory(pathOnly);

                    // Move the file from Previous Document Version Path to 
                    // Current Document Version Path
                    File.Move(filePathPervious, filePathCurrent);

                }

                _documentApprovedService.DocumentApprovalStatus(batchItem.Id,
                                                              out string approvedStatus,
                                                              out int approvedStepNumber);
                if (editUploadedDocumentDTO.IsRejected)
                {
                    DeleteAllDocumentApprovalHistory(trans, currentBatchItemRef);
                    _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Rejected, batch.RequestId, userName);
                    DocumentCheckIn(trans, batchItem.Id, batch.RequestId, userName, userId);
                }
                else if (perviousBatchItem.DocumentClassId != documentClassId && approvedStepNumber < 1)
                {
                    DeleteAllDocumentApprovalHistory(trans, currentBatchItemRef);
                    SaveDocumentApprovalHistory(trans, documentClassId, currentBatchItemRef, out bool checkDocumentTypeHaveNotRoles, batch.RequestId, userName);
                    DocumentCheckIn(trans, batchItem.Id, batch.RequestId, userName, userId);
                }
                //if IsApproved True Then Update DocumentApprovalHistory Entity
                else if (editUploadedDocumentDTO.IsApproved)
                {

                    var UserRoles = _repositorySystemUserRole.Query(x => x.SystemUserId == userId).ToListAsync().Result;
                    var docapphis = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == currentBatchItemRef &&
                                                                                  x.Approved == false).ToListAsync().Result;
                    int roleid = 0;

                    foreach (var itemUserRoles in UserRoles)
                    {
                        foreach (var itemdocapphis in docapphis)
                        {
                            if (itemUserRoles.SystemRoleId == itemdocapphis.RoleId)
                            {
                                roleid = itemUserRoles.SystemRoleId;
                                break;
                            }
                        }
                    }

                    var currentdocapphis = _repositoryDocumentApprovalHistory
                                            .Query(x => x.BatchItemReference == currentBatchItemRef &&
                                                        x.RoleId == roleid).FirstOrDefaultAsync().Result;
                    if (currentdocapphis != null)
                    {
                        currentdocapphis.Approved = true;
                        currentdocapphis.Approvedby = _repositorySystemUser.Query(x => x.Id == userId)
                                                                            .FirstOrDefaultAsync().Result.FullName;

                        _repositoryDocumentApprovalHistory.Update(currentdocapphis);
                        _repositoryDocumentApprovalHistory.SaveChanges(userName, batch.RequestId, trans);
                    }
                    // Update Alert Notification To all Users of Next Approval Step Role 
                    UpdateAlertMessageToRole(trans, batchItem.Id, fileNameCurrent, fileNamePervious, batch.RequestId, userName, userId);

                    //Set BatchItem status 
                    //if Document Approved 
                    if (_documentApprovedService.checkDocumentApproved(batchItem.Id))
                    {
                        //Set BatchItem status Approved
                        _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Approved, batch.RequestId, userName);
                    }
                    else
                    {
                        //Set BatchItem status In Process
                        _statusService.SetBatchItemStatus(batchItem.Id, BatchItemStatusConstants.Checked, batch.RequestId, userName);
                    }
                    DocumentCheckIn(trans, batchItem.Id, batch.RequestId, userName, userId);
                }
                await trans.CommitAsync();
                //log Information with request id of batch if exist
                string logMessage = $"Document Edit With Request Id :: {batch.RequestId}";
                NLogCustomPropertyHelper.LogInformationWithRequestId(_logger, batch?.RequestId, logMessage);

            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                //log error with request id of batch if exist
                string logMessage = $"{e.Message}: {e}";
                NLogCustomPropertyHelper.LogErrorWithRequestId(_logger, batch?.RequestId, logMessage);
                throw;
            }

        }

        private void DeleteAllDocumentApprovalHistory(ITransactionHandler trans, string currentBatchItemRef)
        {
            _repositoryDocumentApprovalHistory.Delete(x => x.BatchItemReference == currentBatchItemRef);
            _repositoryDocumentApprovalHistory.SaveChanges(UserName, null, trans);
        }



        /// <summary>
        /// Update FileName In BatchItem,BatchItemPage,DocumentVersion And BatchMeta Entities
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="perviousBatchItemRef"></param>
        /// <param name="currentBatchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="batchItemId"></param>
        /// <returns></returns>
        public void Update_FileName_And_BatchItemRef_In_BatchItem_And_BatchItemRef_InBatchItemPage_DocumentApprovalHistory_Entities(ITransactionHandler transaction,
                                                                        string perviousBatchItemRef,
                                                                        string currentBatchItemRef,
                                                                        string fileName,
                                                                        int batchItemId,
                                                                        string requestId,
                                                                        string userName)
        {
            try
            {
                //Update FileName In BatchItem Entity
                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefaultAsync().Result;
                batchItem.FileName = fileName;
                _repositoryBatchItem.Update(batchItem);
                _repositoryBatchItem.SaveChanges(userName, requestId, transaction);

                var allHistoryBatchItem = _repositoryBatchItem.Query(x => x.BatchItemReference == perviousBatchItemRef)
                                                                   .ToListAsync().Result;

                //Update BatchItemReference In BatchItem Entity
                allHistoryBatchItem.ForEach(x => x.BatchItemReference = currentBatchItemRef);
                foreach (var item in allHistoryBatchItem)
                {
                    item.BatchItemReference = currentBatchItemRef;
                }
                _repositoryBatchItem.UpdateRange(allHistoryBatchItem);
                _repositoryBatchItem.SaveChanges(userName, requestId, transaction);

                var allHistoryBatchMeta = _repositoryBatchMeta.Query(x => x.BatchItemReference == perviousBatchItemRef)
                                                                 .ToListAsync().Result;
                //Update BatchItemReference In BatchMeta Entity
                foreach (var item in allHistoryBatchMeta)
                {
                    item.BatchItemReference = currentBatchItemRef;
                }
                _repositoryBatchMeta.UpdateRange(allHistoryBatchMeta);
                _repositoryBatchMeta.SaveChanges(userName, requestId, transaction);


                //Update BatchItemReference In DocumentApprovalHistory Entity
                var allHistoryDocumentApprovalHistory = _repositoryDocumentApprovalHistory
                                                        .Query(x => x.BatchItemReference == perviousBatchItemRef)
                                                       .ToListAsync().Result;
                foreach (var item in allHistoryDocumentApprovalHistory)
                {
                    item.BatchItemReference = currentBatchItemRef;
                }
                _repositoryDocumentApprovalHistory.UpdateRange(allHistoryDocumentApprovalHistory);
                _repositoryDocumentApprovalHistory.SaveChanges(userName, requestId, transaction);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Get Previous BatchItem By FileName
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentName"></param>
        /// <returns>BatchItem</returns>
        public BatchItem GetBatchByFileName(ITransactionHandler trans, string documentName)
        {
            try
            {
                var queryWithFileName = _repositoryBatchItem.Query(x => x.FileName == documentName)
                                                  .Include(x => x.Batch).ToListAsync().Result;

                BatchItem batchItemWithFileName = new();
                if (queryWithFileName.Count != 0)
                {
                    batchItemWithFileName = queryWithFileName[^1];
                }
                return batchItemWithFileName;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }

        }

        /// <summary>
        /// Remove "documentId" Previous Document Version From Document CheckOut And 
        /// Add  "batchItemId" Current Document Version In Document Checkout
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentId"></param>
        /// <param name="batchItemId"></param>
        /// <returns></returns>
        public void UpdateCheckoutDcouments(ITransactionHandler trans,
                                            int documentId,
                                            int batchItemId,
                                            string requestId,
                                            string userName,
                                            int userId)
        {
            try
            {
                DocumentCheckIn(trans, documentId, requestId, userName, userId);

                DocumentCheckOut(trans, batchItemId, requestId, userName, userId);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Add Latest "batchItemId" Document Version In Document Checkout
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <returns></returns>
        public void DocumentCheckOut(ITransactionHandler trans,
                                     int batchItemId,
                                     string requestId,
                                     string userName,
                                     int userId)
        {
            try
            {
                DocumentsCheckedOut documentsCheckedOut = new()
                {
                    //creating dto for insert record and response
                    BatchItemId = batchItemId,
                    SystemUserId = userId
                };

                //creating new instance 
                _repositoryDocumentsCheckedOut.Insert(documentsCheckedOut);
                _repositoryDocumentsCheckedOut.SaveChanges(userName, requestId, trans);

                //creating new instance to insert log
                DocumentsCheckedOutLog checkedOutLog = new()
                {
                    BatchItemId = batchItemId,
                    SystemUserId = userId,
                    CheckedOutAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    CheckedInAt = 0
                };
                _repositoryDocumentsCheckedOutLog.Insert(checkedOutLog);
                _repositoryDocumentsCheckedOutLog.SaveChanges(userName, requestId, trans);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Remove "documentId" Previous Version From Document CheckOut And 
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public void DocumentCheckIn(ITransactionHandler trans, int documentId, string requestId, string userName, int userId)
        {
            try
            {
                var entity = _repositoryDocumentsCheckedOut.Query(x => x.BatchItemId == documentId)
                                                            .FirstOrDefaultAsync()
                                                             .Result;
                // deleting the existing record from db if doc gets check in 
                _repositoryDocumentsCheckedOut.Delete(entity.Id, false);
                _repositoryDocumentsCheckedOut.SaveChanges(userName, requestId, trans);

                //Update log
                DocumentsCheckedOutLog checkedInLog = _repositoryDocumentsCheckedOutLog.Query(x => x.BatchItemId == documentId)
                                                                                     .FirstOrDefaultAsync()
                                                                                      .Result;
                if (checkedInLog != null)
                {
                    checkedInLog.SystemUserId = userId;
                    checkedInLog.CheckedInAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    _repositoryDocumentsCheckedOutLog.Update(checkedInLog);
                    _repositoryDocumentsCheckedOutLog.SaveChanges(userName, requestId, null);
                }
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }


        /// <summary>
        /// Get Previous Document Version
        /// </summary>
        /// <param name="documentVersionId"></param>
        /// <returns>DocumentVersion</returns>
        public DocumentVersion GetPreviousDocumentVersion(int documentVersionId)
        {
            try
            {
                var DocumentVersion = _repositoryDocumentVersion.Query(x => x.Id == documentVersionId)
                                                                         .FirstOrDefaultAsync().Result;
                return DocumentVersion;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Save Document Version With Increment Version Number
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="BatchId"></param>
        /// <param name="perviousVerionId"></param>
        /// <param name="comments"></param>
        /// <returns>Document Version</returns>
        public DocumentVersion GetIncrementDocumentVersion(ITransactionHandler trans,
                                                           int BatchId,
                                                           int perviousVerionId,
                                                           string comments,
                                                           string requestId,
                                                           string userName,
                                                           int userId)
        {
            try
            {
                // If not create new batch and return
                var documentVersion = new DocumentVersion
                {
                    Comments = comments,
                    LastModifiedBy = userId.ToString(),
                    Version = perviousVerionId + 1
                };
                _repositoryDocumentVersion.Insert(documentVersion);
                _repositoryDocumentVersion.SaveChanges(userName, requestId, trans);
                return documentVersion;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Save Document Version
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <returns>DocumentVersion</returns>
        public DocumentVersion GetDocumentVersion(ITransactionHandler trans, string requestId,
                                           string userName, int userId)
        {
            try
            {
                // If not create new batch and return
                var documentVersion = new DocumentVersion
                {
                    Version = 1,
                    Comments = "",
                    LastModifiedBy = userId.ToString()
                };

                _repositoryDocumentVersion.Insert(documentVersion);
                _repositoryDocumentVersion.SaveChanges(userName, requestId, trans);
                return documentVersion;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                           string userName)
        {
            try
            {
                // If existing batch of client exists return the batch
                var query = _repositoryBatch.Query(x => x.CustomerId == clientId);
                var batch = await query.FirstOrDefaultAsync();

                // If found return batch
                if (batch != null) return batch;

                // If not create new batch and return
                var batchDTO = new BatchDTO
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

                Batch entityBatch = _mapper.Map<Batch>(batchDTO);
                _repositoryBatch.Insert(entityBatch);
                _repositoryBatch.SaveChanges(userName, entityBatch.RequestId, trans);

                return entityBatch;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                        string requestId,
                                        string userName)
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
                _repositoryBatchItem.SaveChanges(userName, requestId, transaction);

                return batchItem;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Save New BatchItem And Change FileName In Previous BatchItems
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchId"></param>
        /// <param name="documentVersionId"></param>
        /// <param name="documentClassId"></param>
        /// <param name="fileName"></param>
        /// <param name="companyId"></param>
        /// <returns>Batch Item</returns>
        public BatchItem UpdateBatchItem(ITransactionHandler transaction,
                                        int batchId,
                                        int documentVersionId,
                                        int documentClassId,
                                        string perviousBatchItemRef,
                                        int perviousBatchItemId,
                                        int perviousBatchItemSataus,
                                        int? perviousBatchItemRoleId,
                                        string fileName,
                                        int companyId,
                                        bool isApproved,
                                        string requestId,
                                        string userName)
        {
            try
            {
                int userRole = _documentApprovedService.GetUserRoleDocumentApprovedHistory(perviousBatchItemId);


                var batchItem = new BatchItem
                {
                    BatchId = batchId,
                    DocumentVersionId = documentVersionId,
                    BatchItemReference = perviousBatchItemRef,
                    FileName = fileName,
                    DocumentClassId = documentClassId,
                    OccuredAt = DateTime.Now,
                    ParentId = null,
                    BatchItemStatusId = perviousBatchItemSataus,
                    SystemRoleId = perviousBatchItemRoleId,
                    CompanyId = companyId
                };



                if (userRole != 0 && isApproved == true)
                {
                    batchItem.SystemRoleId = userRole;
                }

                _repositoryBatchItem.Insert(batchItem);
                _repositoryBatchItem.SaveChanges(userName, requestId, transaction);


                var allHistory = _repositoryBatchItem.Query(x => x.BatchItemReference == perviousBatchItemRef).ToListAsync().Result;
                foreach (var item in allHistory)
                {
                    item.BatchId = batchId;
                    _repositoryBatchItem.Update(item);
                    _repositoryBatchItem.SaveChanges(userName, requestId, transaction);

                }

                return batchItem;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                       string requestId,
                                       string userName,
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
                _repositoryBatchIPage.SaveChanges(userName, requestId, transaction);
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                         string requestId,
                                         string userName)
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
                    _repositoryBatchIFields.SaveChanges(userName, requestId, transaction);
                }
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
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
                                       string requestId,
                                       string userName)
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
                    _repositoryBatchMeta.SaveChanges(userName, requestId, transaction);
                }
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }
    }
}
