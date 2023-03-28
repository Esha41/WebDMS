using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Api.Services.DocumentApproved;
using Intelli.DMS.Api.Services.DocumentCheckOut;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Intelli.DMS.Api.Services.ClientRepository.Impl
{
    public class ClientRepositoryService : BaseService, IClientRepositoryService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICheckOutService _checkOutService;
        private readonly IDocumentApprovedService _documentApprovedService;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<DocumentApprovalHistory> _repositoryDocumentApprovalHistory;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;
        private readonly IRepository<Client> _repositoryClient;
        private readonly IRepository<SystemUser> _repositorySystemUser;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<BatchItemStatus> _repositoryBatchItemStatus;
        private readonly IRepository<BatchStatus> _repositoryBatchStatus;
        private readonly IRepository<BatchItemField> _repositoryBatchItemField;
        private readonly IRepository<BatchItemPage> _repositoryBatchItemPage;
        private readonly IRepository<DocumentClass> _repositoryDocumentClass;
        private readonly IRepository<DocumentClassField> _repositoryDocumentClassFields;
        private readonly IRepository<DocumentCheckOutLogsView> _repositoryDocumentCheckOutLogsView;



        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepositoryService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="documentApprovedService">Instance of <see cref="IDocumentApprovedService"/> will be injected</param>
        /// <param name="checkOutService">Instance of <see cref="ICheckOutService"/> will be injected</param>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/> will be injected</param>
        public ClientRepositoryService(DMSContext context,
            IMapper mapper,
            ILogger<ClientRepositoryService> logger,
            IDocumentApprovedService documentApprovedService,
            ICheckOutService checkOutService,
            IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryDocumentApprovalHistory = new GenericRepository<DocumentApprovalHistory>(context);
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _repositorySystemUser = new GenericRepository<SystemUser>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchItemField = new GenericRepository<BatchItemField>(context);
            _repositoryBatchItemPage = new GenericRepository<BatchItemPage>(context);
            _repositoryBatchItemStatus = new GenericRepository<BatchItemStatus>(context);
            _repositoryDocumentClass = new GenericRepository<DocumentClass>(context);
            _repositoryClient = new GenericRepository<Client>(context);
            _repositoryBatchStatus = new GenericRepository<BatchStatus>(context);
            _repositoryDocumentCheckOutLogsView = new GenericRepository<DocumentCheckOutLogsView>(context);
            _repositoryDocumentClassFields = new GenericRepository<DocumentClassField>(context);
            _mapper = mapper;
            _logger = logger;
            _documentApprovedService = documentApprovedService;
            _checkOutService = checkOutService;
        }

        /// <summary>
        /// Get Client Detail With RepositoryId
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="entity">Instance of ClientRepositoryView Entity</param>
        /// <returns></returns>
        public ClientDTO GetClientDetailWithRepositoryId(int repositoryId, ClientRepositoryView entity)
        {
            try
            {
                var customer = _repositoryClient.Query(x => x.Id == entity.ClientId)?.FirstOrDefault();
                var batch = _repositoryBatch.Query(x => x.CustomerId == entity.ClientId)?.FirstOrDefault();
                var documentList = _repositoryBatchItem.Query(x => x.BatchId == repositoryId)
                                                    .Include(x => x.BatchItemPages)
                                                    .Include(x => x.DocumentVersion)
                                                    .Include(x => x.BatchItemStatus)
                                                    .Include(x=>x.DocumentClass)
                                                    .ToList().OrderByDescending(x => x.Id).ToList();
                List<ClientDocumentListDTO> clientDocumentListDTOs = new();
                List<string> batchItemRefs = new();
                int lastModifiedBy = 0;
                int checkoutById = 0;
                foreach (var item in documentList)
                {
                    if (!batchItemRefs.Contains(item.BatchItemReference))
                    {
                        lastModifiedBy = Convert.ToInt32(item.DocumentVersion.LastModifiedBy);

                        ClientDocumentListDTO clientDocumentListDTO = new();
                        clientDocumentListDTO.Id = item.Id;
                        clientDocumentListDTO.Name = item.FileName;
                        clientDocumentListDTO.Version = item.DocumentVersion.Version.ToString();
                        clientDocumentListDTO.StatusTime = item.CreatedAt;
                        clientDocumentListDTO.LastModifiedOn = item.UpdatedAt;
                        clientDocumentListDTO.DocumentClassName = item.DocumentClass?.DocumentClassName;

                        if (lastModifiedBy != UserId)
                        {
                            if (_checkOutService.IsDocumentCheckOut(item.Id) == 0)
                            {
                                if (_documentApprovedService.checkDocumentApproved(item.Id) ||
                                    !_documentApprovedService.checkUserRoleHaveDocumentCheckoutAccess(item.Id))
                                {
                                    clientDocumentListDTO.IsCheckOutDocument = true;
                                }
                                else
                                {
                                    clientDocumentListDTO.IsCheckOutDocument = false;
                                }
                            }
                            else
                            {
                                clientDocumentListDTO.IsCheckOutDocument = true;
                                checkoutById = _repositoryDocumentsCheckedOut.Query(x => x.BatchItemId == item.Id)
                                                                              .FirstOrDefault().SystemUserId;
                                clientDocumentListDTO.CheckoutBy = _repositorySystemUser.Query(x => x.Id == checkoutById)?
                                                                                         .FirstOrDefault()?.FullName;
                            }
                        }
                        else
                        {
                            clientDocumentListDTO.IsCheckOutDocument = true;
                        }

                        _documentApprovedService.DocumentApprovalStatus(item.Id,
                                                                      out string approvedStatus,
                                                                      out int approvedStepNumber);

                        if (approvedStatus == DocumentApprovedStatusConstants.Pending &&
                            approvedStepNumber != 0)
                        {
                            clientDocumentListDTO.State = $"Approved Level {approvedStepNumber}";
                        }
                        else
                        {
                            clientDocumentListDTO.State = _repositoryBatchItemStatus.Query(x => x.Id == item.BatchItemStatusId)
                                                                                    .FirstOrDefault().BatchItemStatusName;
                        }
                        clientDocumentListDTOs.Add(clientDocumentListDTO);
                    }
                    batchItemRefs.Add(item.BatchItemReference);
                }
                ClientDTO clientDTO = new()
                {
                    Id = (customer == null) ? 0 : customer.Id,
                    FirstName = customer?.FirstName,
                    LastName = customer?.LastName,
                    ExternalId = customer?.ExternalId,
                    JMBG = customer?.JMBG,
                    IsActive = customer?.IsActive,
                    Reason = customer?.Reason,
                    ClientStatus = _repositoryBatchStatus.Query(x => x.Id == batch.BatchStatusId).FirstOrDefault().BatchStatusName,
                    IsNotValidForTransaction = customer != null && customer.IsNotValidForTransaction,
                    CustomerDocuments = clientDocumentListDTOs
                };
                return clientDTO;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        public List<DocumentVersionListDTO> MakeDocumentVersionList(int documentId)
        {
            try
            {
                // Get BatchItemReference W.r.t documentId
                var batachItemRef = _repositoryBatchItem.Query(x => x.Id == documentId).FirstOrDefault().BatchItemReference;

                // Get BatchItem List W.r.t documentId
                var documentList = _repositoryBatchItem.Query(x => x.BatchItemReference == batachItemRef)
                                                     .Include(x => x.DocumentVersion)
                                                     .ToList().ToList();
                List<DocumentVersionListDTO> documentVersionListDTOs = new();
                int userId = 0;
                int approvedLevelNumber = 1;
                int doItOnces = 0;
                List<int?> checkRoleList = new();
                foreach (var item in documentList)
                {
                    // Set UserId Receive From Document Version Entity
                    userId = Convert.ToInt32(item.DocumentVersion.LastModifiedBy);

                    // Create Object of  DocumentVersionListDTO
                    DocumentVersionListDTO documentVersionListDTO = new()
                    {
                        ID = item.Id,
                        BatchItemId = item.Id,
                        Name = item.FileName,
                        Description = item.DocumentVersion.Comments,
                        Version = item.DocumentVersion.Version,
                        VersionId = item.DocumentVersion.Id,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        LastModifiedBy = _repositorySystemUser.Query(x => x.Id == userId)?
                                                                                  .FirstOrDefault()?.FullName,
                    };

                    // Get DocumentApprovalStatus By Get Status of approvedStatus and approvedStepNumber
                    // of the Document 
                    _documentApprovedService.DocumentApprovalStatus(item.Id,
                                                                  out string approvedStatus,
                                                                  out int approvedStepNumber);

                    // if approvedStepNumber Not Equal to 0 and Document Status 
                    //  Equal to Checked
                    if (approvedStepNumber != 0 &&
                        item.BatchItemStatusId == BatchItemStatusConstants.Checked
                       )
                    {
                        // check for Approved Level 1
                        if (doItOnces == 0)
                        {
                            // Add SystemRoleId in checkRoleList
                            checkRoleList.Add(item.SystemRoleId);
                            // Set Document State Approved Level 1
                            documentVersionListDTO.State = $"Approved Level {approvedLevelNumber}";
                            doItOnces++;
                        }
                        // Check for Approved Level 2,3,4 and so on 
                        else if (!(checkRoleList.Contains(item.SystemRoleId)))
                        {
                            approvedLevelNumber++;
                            checkRoleList.Add(item.SystemRoleId);
                            documentVersionListDTO.State = $"Approved Level {approvedLevelNumber}";
                        }
                        // Check for Similar Approved Levels
                        else
                        {
                            documentVersionListDTO.State = $"Approved Level {approvedLevelNumber}";
                        }
                    }
                    // Other than Checkd Status of  Document  
                    else
                    {
                        // Get Document Status from BatchItemStatus W.r.t to Document BatchItemStatusId 
                        documentVersionListDTO.State = _repositoryBatchItemStatus.Query(x => x.Id == item.BatchItemStatusId)
                                                                                    .FirstOrDefault().BatchItemStatusName;
                    }
                    documentVersionListDTOs.Add(documentVersionListDTO);
                }
                return documentVersionListDTOs.OrderByDescending(x => x.ID).ToList();
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }
        /// <summary>
        /// Get Document Field Values
        /// </summary>
        /// <param name="batchItemId">batchItemId</param>
        /// <param name="entity">out parameter Instance of BatchItem</param>
        /// <param name="customer">out parameter Instance of Client</param>
        /// <param name="documentClass">out parameter Instance of DocumentClass</param>
        /// <param name="isDocumentClassModifiable">out parameter isDocumentClassModifiable</param>
        /// <param name="documentFieldValueDTOs">out parameter List of DocumentFieldValueDTO</param>
        public void GetDocumentFieldValues(int batchItemId,
                                            out BatchItem entity,
                                            out Client customer,
                                            out DocumentClass documentClass,
                                            out bool isDocumentClassModifiable,
                                            out List<DocumentFieldValueDTO> documentFieldValueDTOs
            )
        {
            try
            {
                // Get BatchItem W.r.t batchItemId
                var query = _repositoryBatchItem.Query(x => x.Id == batchItemId)
                                                .Include(x => x.BatchItemFields)
                                                .Include(x => x.BatchItemPages)
                                                .Include(x => x.DocumentClass)
                                                .Include(x => x.Batch);

                entity = query?.FirstOrDefault();
                var batchItem = entity;

                // Check the Document Status is Rejected
                if (entity.BatchItemStatusId == BatchItemStatusConstants.Rejected)
                {
                    // Get BatchItem W.r,t to BatchItemReference and Document Version = 1 
                    query = _repositoryBatchItem.Query(x => x.BatchItemReference == batchItem.BatchItemReference
                                                         && x.DocumentVersion.Version == 1)
                                                .Include(x => x.BatchItemFields)
                                                .Include(x => x.BatchItemPages)
                                                .Include(x => x.DocumentClass)
                                                .Include(x => x.Batch);
                    entity = query?.FirstOrDefault();
                }
                // Get BatchId Of Document
                int batchId = entity.BatchId;

                // Get BatchItemId Of Document
                int entitybatchItemId = entity.Id;

                // Get BatchItemReference Of Document
                string batchItemRef = entity.BatchItemReference;

                //Get DocumentTypeId of Document
                int documentTypeId = entity.DocumentClass.DocumentTypeId;

                //Get Batch Entity W.r.t BatchId
                var batchEntity = _repositoryBatch.Query(x => x.Id == batchId)?.FirstOrDefault();

                // Get Client Entity W.r.t Batch's CustomerId
                customer = _repositoryClient.Query(x => x.Id == batchEntity.CustomerId)?.FirstOrDefault();

                // Get DocumentClassId of Document 
                int? documentClassId = entity.DocumentClassId;

                // Get DocumentClass Entity W.r.t DocumentClassId Of Document
                documentClass = _repositoryDocumentClass.Query(x => x.Id == documentClassId)?
                                                            .FirstOrDefault();
                
                //all document class fields of this document class
                var allDocumentClassFields = _repositoryDocumentClassFields.Query(x => x.DocumentClassId == documentClassId).ToList();
                
                // Get 1st DocumentApprovalHistory W.r.t BatchItemReference of Document
                // and where Approved == true 
                var checkDocumentAppHistory = _repositoryDocumentApprovalHistory.Query(x => x.Approved == true &&
                                                                                          x.BatchItemReference == batchItemRef)
                                                                                  .FirstOrDefault();

                // Get Count Of all DocumentApprovalHistory W.r.t BatchItemReference 
                var checkDocumentAppHistoryAllCount = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItemRef)
                                                                                          .ToList().Count;

                // 
                if (checkDocumentAppHistory != null || checkDocumentAppHistoryAllCount == 0)
                {
                    isDocumentClassModifiable = false;
                }
                else
                {
                    isDocumentClassModifiable = true;
                }
                // Get All BatchItemFields W.r.t BatchItemId
                var documentClassFields = _repositoryBatchItemField.Query(x => x.BatchItemId == entitybatchItemId)
                                                              .Include(x => x.DocumentClassField)
                                                                .ThenInclude(x => x.DictionaryType)
                                                                 .ThenInclude(x => x.BopDictionaries);

                documentFieldValueDTOs = new List<DocumentFieldValueDTO>();
                foreach (var item in documentClassFields)
                {
                    string selectedDictionaryTypeName = "";
                    var dictionary = _mapper.Map<DictionaryTypeDTO>(item.DocumentClassField.DictionaryType);
                    // if DocumentClassField is DictionaryType
                    if (item.DocumentClassField.DictionaryType != null)
                    {
                        // Set Selected Dictionary Type Name
                        int? bopDictionaryId = Convert.ToInt32(item.RegisteredFieldValue);
                        if (bopDictionaryId != 0)
                        {
                            selectedDictionaryTypeName = item?.DocumentClassField?.DictionaryType
                                                                             ?.BopDictionaries
                                                                              ?.Where(x => x.Id == bopDictionaryId)
                                                                               ?.FirstOrDefault()!.Value;
                            dictionary.SelectedDictionaryTypeName = selectedDictionaryTypeName;
                        }
                    }
                    DocumentFieldValueDTO documentFieldValueDTO = new()
                    {
                        Id = item.DocumentClassFieldId,
                        DocumentClassId = item.DocumentClassField.DocumentClassId,
                        DocumentClassFieldTypeId = item.DocumentClassField.DocumentClassFieldTypeId,
                        Uilabel = item.DocumentClassField?.Uilabel,
                        UISort = item.DocumentClassField?.UISort,
                        DictionaryTypeId = item.DocumentClassField.DictionaryTypeId,
                        IsMandatory = item.DocumentClassField.IsMandatory,
                        IsActive = item.IsActive,
                        MinLength = item.DocumentClassField.MinLength,
                        MaxLength = item.DocumentClassField.MaxLength,
                        DocumentClassFieldId = item.DocumentClassFieldId,
                        DictionaryValueId = item.DocumentClassField.DictionaryTypeId,
                        FieldValue = item.RegisteredFieldValue,
                        DictionaryType = dictionary,
                    };



                    // DocumentClassFieldTypeId Equal to ExpirationDate
                    if (item.DocumentClassField.DocumentClassFieldTypeId == 10)
                    {
                        // Set ExpirationDate value to proper format
                        bool checkLongConvertion = long.TryParse(item.RegisteredFieldValue, out long unixLong);
                        if (checkLongConvertion != false)
                        {
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixLong / 1000);
                            DateTime date = dateTimeOffset.DateTime.AddDays(1);
                            if (date.Month < 10)
                            {
                                if (date.Day < 10)
                                {
                                    documentFieldValueDTO.FieldValue = $"{date.Year}-0{date.Month}-0{date.Day}";
                                }
                                else
                                {
                                    documentFieldValueDTO.FieldValue = $"{date.Year}-0{date.Month}-{date.Day}";
                                }
                            }
                            else
                            {
                                if (date.Day < 10)
                                {
                                    documentFieldValueDTO.FieldValue = $"{date.Year}-{date.Month}-0{date.Day}";
                                }
                                else
                                {
                                    documentFieldValueDTO.FieldValue = $"{date.Year}-{date.Month}-{date.Day}";
                                }
                            }
                        }
                    }

                    documentFieldValueDTOs.Add(documentFieldValueDTO);

                }
                var itemsDocumentClassFields = documentFieldValueDTOs.OrderBy(x => x.UISort).ToList();
                var getItemOfUIsortNull = itemsDocumentClassFields.Where(x => x.UISort == null).ToList();
                var getItemOfUIsortNotNull = itemsDocumentClassFields.Where(x => x.UISort != null).ToList();
                List<DocumentFieldValueDTO> documentClassAllFieldsDTOs = new();
                documentClassAllFieldsDTOs.AddRange(getItemOfUIsortNotNull);
                documentClassAllFieldsDTOs.AddRange(getItemOfUIsortNull);
                documentFieldValueDTOs = documentClassAllFieldsDTOs;

                var tempFieldList = documentFieldValueDTOs;
                var fields = allDocumentClassFields.Where(x => !tempFieldList.Any(c => c.DocumentClassFieldId == x.Id)).ToList();
               
                documentFieldValueDTOs.AddRange(_mapper.Map<List<DocumentFieldValueDTO>>(fields));
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Get Client Document With ClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="repositoryRoot"></param>
        /// <returns></returns>
        public ClientDocumentsOldDTO GetClientDocumentWithClientId(int clientId, string repositoryRoot)
        {
            try
            {
                var result = from batchPages in _repositoryBatchItemPage.Query(s => s.IsActive == true)
                                                                                    .Include(a => a.BatchItem)
                                                                                    .ThenInclude(a => a.Batch)
                                                                                    .ThenInclude(a => a.Customer)
                             join batchItems in _repositoryBatchItem.Query(s => s.Batch.CustomerId == clientId)
                             on batchPages.BatchItemId equals batchItems.Id
                             select batchPages;

                //setting response
                ClientDocumentsOldDTO response = new();
                var getCustomer = result.FirstOrDefault().BatchItem.Batch.Customer;
                response.CustomerId = clientId;
                response.CustomerName = getCustomer.FirstName + " " + getCustomer.LastName;
                response.ExternalId = getCustomer.ExternalId;

                //generating base64string of all files
                foreach (var items in result.ToList())
                {
                    //getting full file path
                    var filePath = RepositoryHelper.BuildUrlFilePath(repositoryRoot,
                                                                          items.BatchItem.BatchId,
                                                                          items.FileName,
                                                                          items.BatchItem.Batch.CreatedDate);

                    //if file directory exist in server too then return
                    if (Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        // converting to base 64 string
                        byte[] fileByte = System.IO.File.ReadAllBytes(filePath);
                        string base64String = Convert.ToBase64String(fileByte);

                        FileContentOldDTO fileContent = new();
                        fileContent.FileName = items.FileName;
                        fileContent.FileInBase64 = base64String;
                        response.Documents.Add(fileContent);
                    }
                }

                return response;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Get Document Checkout History
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public List<DocumentCheckOutLogsView> GetDocumentCheckoutHistory(int documentId)
        {
            try
            {
                var batachItemRef = _repositoryBatchItem.Query(x => x.Id == documentId)
                                                         .FirstOrDefault()
                                                          .BatchItemReference;
                var batchItemIds = _repositoryBatchItem.Query(x => x.BatchItemReference == batachItemRef)
                                                        .Select(a => a.Id)
                                                         .ToList();
                var documentCheckoutHistory = _repositoryDocumentCheckOutLogsView.Query(x => batchItemIds.Contains(x.BatchItemId))
                                                                                   .ToList();
                return documentCheckoutHistory;
            }
            catch (Exception e)
            {

                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }
    }
}

