using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentUpload
{
    public interface IDocumentUploadService
    {
        /// <summary>
        /// Upload Contract
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns> Task </returns>

        Task UploadContract(UploadContractDTO dto,
                                string userName, int userId);

        /// <summary>
        /// DocumentUpload
        /// </summary>
        /// <param name="file"> Instance of IFormFile </param>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="companyId"></param>
        /// <param name="httpBatchMetaList"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns> Task </returns>

        Task DocumentUpload(IFormFile file,
                                          int documentClassId,
                                          int clientId,
                                          int companyId,
                                          StringValues httpBatchMetaList,
                                           string userName, int userId);
        /// <summary>
        /// Document Edit
        /// </summary>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="editUploadedDocumentDTO">Object of EditUploadedDocumentDTO</param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns> Task </returns>

        Task DocumentEdit(int documentClassId,
                               int clientId,
                               EditUploadedDocumentDTO editUploadedDocumentDTO,
                                           string userName, int userId);
        /// <summary>
        /// Save Alert Message To Role
        /// </summary>
        /// <param name="transaction">Instance Of <see cref="ITransactionHandler"/> </param>
        /// <param name="batchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>

        void SaveAlertMessageToRole(ITransactionHandler transaction,
                                    int batchItemRef,
                                    string fileName,
                                    string requestId,
                                    string userName,
                                    int userId);

        /// <summary>
        /// Update Alert Message To Role
        /// </summary>
        /// <param name="transaction"> Instance of <see cref="ITransactionHandler"/> </param>
        /// <param name="batchItemId"></param>
        /// <param name="currentFileName"></param>
        /// <param name="perviousFileName"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>

        void UpdateAlertMessageToRole(ITransactionHandler transaction,
                                             int batchItemId,
                                             string currentFileName,
                                             string perviousFileName,
                                             string requestId,
                                             string userName,
                                             int userId);
        /// <summary>
        ///  Save Document Approval History
        /// </summary>
        /// <param name="trans"> Instance of <see cref="ITransactionHandler"/> </param>
        /// <param name="documentClassId"></param>
        /// <param name="batchItemRef"></param>
        /// <param name="documentTypeHaveNotApprovalStepRoles"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>

        void SaveDocumentApprovalHistory(ITransactionHandler trans,
                                                int documentClassId,
                                                string batchItemRef,
                                                out bool documentTypeHaveNotApprovalStepRoles,
                                                string requestId,
                                           string userName);

        /// <summary>
        ///  Update BatchItem Reference In BatchItem
        /// </summary>
        /// <param name="transaction">Instance of ITransactionHandler </param>
        /// <param name="batchItemId"></param>
        /// <param name="batchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>

        void UpdateBatchItemRefInBatchItem(ITransactionHandler transaction,
                                             int batchItemId,
                                             string batchItemRef,
                                             string fileName,
                                             string requestId,
                                           string userName);


        /// <summary>
        /// Update FileName And BatchItemRef In BatchItem 
        /// ,BatchItemRef In BatchItemPage And 
        /// In DocumentApprovalHistory Entities
        /// </summary>
        /// <param name="transaction">Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="perviousBatchItemRef"></param>
        /// <param name="currentBatchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="batchItemId"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>

        void Update_FileName_And_BatchItemRef_In_BatchItem_And_BatchItemRef_InBatchItemPage_DocumentApprovalHistory_Entities(ITransactionHandler transaction,
                                                                        string perviousBatchItemRef,
                                                                        string currentBatchItemRef,
                                                                        string fileName,
                                                                        int batchItemId,
                                                                        string requestId,
                                                                        string userName);

        /// <summary>
        ///  Get Batch By FileName
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="documentName"></param>
        /// <returns></returns>

        BatchItem GetBatchByFileName(ITransactionHandler trans, string documentName);

        /// <summary>
        /// Remove "documentId" Previous Document Version From Document CheckOut And 
        /// Add  "batchItemId" Current Document Version In Document Checkout
        /// </summary>
        /// <param name="trans"> Instance Of ITransactionHandler </param>
        /// <param name="documentId"></param>
        /// <param name="batchItemId"></param>
        /// <param name="RequestId"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        void UpdateCheckoutDcouments(ITransactionHandler trans, int documentId, int batchItemId, string RequestId,
                                           string userName, int userId);

        /// <summary>
        /// Add Latest "batchItemId" Document Version In Document Checkout
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <param name="requestId"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        void DocumentCheckOut(ITransactionHandler trans, int batchItemId, string requestId,
                                           string userName, int userId);

        /// <summary>
        /// Remove "documentId" Previous Version From Document CheckOut And 
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentId">documentId</param>
        /// <param name="RequestId">RequestId</param>
        /// <param name="UserName">UserName</param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        void DocumentCheckIn(ITransactionHandler trans, int documentId, string RequestId,
                                           string UserName, int UserId);

        /// <summary>
        /// Get Previous Document Version
        /// </summary>
        /// <param name="DocumentVersionId"></param>
        /// <returns>DocumentVersion</returns>

        DocumentVersion GetPreviousDocumentVersion(int DocumentVersionId);

        /// <summary>
        /// Save Document Version With Increment Version Number
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="BatchId"></param>
        /// <param name="perviousVerionId"></param>
        /// <param name="comments"></param>
        /// <param name="requestId">RequestId</param>
        /// <param name="userName">UserName</param>
        /// <param name="userId"></param>
        /// <returns>Document Version</returns>

        DocumentVersion GetIncrementDocumentVersion(ITransactionHandler trans,
                                                             int BatchId,
                                                             int perviousVerionId,
                                                             string comments,
                                                             string requestId,
                                                             string userName,
                                                             int userId);
        /// <summary>
        /// Save Document Version
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="requestId">requestId </param>
        /// <param name="userName"> UserName</param>
        /// <param name="userId"></param>
        /// <returns>DocumentVersion</returns>

        DocumentVersion GetDocumentVersion(ITransactionHandler trans,
                                           string requestId,
                                           string userName,
                                           int userId);

        /// <summary>
        /// Save batch
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="clientId"></param>
        /// <param name="companyId"></param>
        /// <param name="userName"> UserName</param>
        /// <returns>Task of Batch </returns>

        Task<Batch> GetBatch(ITransactionHandler trans,
                                           int clientId,
                                           int companyId,
                                           string userName);
        /// <summary>
        /// Save batch item
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchId"></param>
        /// <param name="documentClassId"></param>
        /// <returns>BatchItem</returns>

        BatchItem SaveBatchItem(ITransactionHandler transaction,
                                        int batchId,
                                        int documentVersionId,
                                        int documentClassId,
                                        int companyId,
                                        string requestId,
                                        string userName);

        /// <summary>
        /// Update Batch Item
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="batchId"></param>
        /// <param name="documentVersionId"></param>
        /// <param name="documentClassId"></param>
        /// <param name="perviousBatchItemRef"></param>
        /// <param name="perviousBatchItemId"></param>
        /// <param name="perviousBatchItemSataus"></param>
        /// <param name="perviousBatchItemRoleId"></param>
        /// <param name="fileName"></param>
        /// <param name="companyId"></param>
        /// <param name="isApproved"></param>
        /// <param name="requestId"></param>
        /// <returns>BatchItem</returns>
        BatchItem UpdateBatchItem(ITransactionHandler transaction,
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
                                        string userName);
        /// <summary>
        /// Save batch item page
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <param name="fileName"></param>
        /// <param name="requestId"></param>
        /// <param name="pageNo"></param>

        void SaveBatchItemPage(ITransactionHandler transaction,
                                       int batchItemId,
                                       int documentVersionId,
                                       string fileName,
                                       string originalFileName,
                                       string requestId,
                                       string userName,
                                       int pageNo = 1);
        /// <summary>
        /// Save batch item fields
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchMetaList"></param>
        /// <param name="batchItemId"></param>
        /// <param name="RequestId"></param>

        void SaveBatchItemFields(ITransactionHandler transaction,
                                         List<BatchMetaDTO> batchMetaList,
                                         int batchItemId,
                                         int documentVersionId,
                                         string RequestId,
                                         string UserName);
        /// <summary>
        /// Save batch item meta
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchMetaList"></param>
        /// <param name="batchId"></param>
        /// <param name="requestId"></param>

        void SaveBatchItemMeta(ITransactionHandler transaction,
                                       List<BatchMetaDTO> batchMetaList,
                                       int batchId,
                                       int documentVersionId,
                                       string fileName,
                                       string batchItemRef,
                                       string requestId,
                                       string userName);
    }
}
