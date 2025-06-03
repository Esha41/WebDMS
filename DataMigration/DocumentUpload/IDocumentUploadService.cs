using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMigration.DocumentUpload
{
    public interface IDocumentUploadService
    {
        /// <summary>
        /// Document Upload
        /// </summary>
        /// <param name="file"> Instance of <see cref="IFormFile"/></param>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <returns>Document Version</returns>

        Task DocumentUpload(IFormFile file,
                                          int documentClassId,
                                          int clientId,
                                          int companyId,
                                          StringValues httpBatchMetaList,
                                           string UserName, int UserId, string localsavePath);

        /// <summary>
        /// Save Alert Message To that Role who current Review This Document
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="RequestId"></param>
        /// <returns></returns>

        void SaveAlertMessageToRole(ITransactionHandler transaction,
                                    int batchItemRef,
                                    string fileName,
                                    string RequestId,
                                    string UserName,
                                    int UserId);

        /// <summary>
        /// Save Document Approval History 
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="documentClassId"></param>
        /// <param name="batchItemRef"></param>
        /// <param name="documentTypeHaveNotApprovalStepRoles"></param>
        /// <param name="RequestId"></param>
        /// <returns></returns>

        void SaveDocumentApprovalHistory(ITransactionHandler trans,
                                                int documentClassId,
                                                string batchItemRef,
                                                out bool documentTypeHaveNotApprovalStepRoles,
                                                string RequestId,
                                           string UserName);

        /// <summary>
        /// Save FileName In BatchItem
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <param name="BatchItemRef"></param>
        /// <param name="fileName"></param>
        /// <param name="RequestId"></param>
        /// <returns></returns>

        void UpdateBatchItemRefInBatchItem(ITransactionHandler transaction,
                                             int batchItemId,
                                             string BatchItemRef,
                                             string fileName,
                                             string RequestId,
                                           string UserName);


        /// <summary>
        /// Save Document Version
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="requestId">requestId </param>
        /// <param name="UserName"> UserName</param>
        /// <returns>DocumentVersion</returns>

        DocumentVersion GetDocumentVersion(ITransactionHandler trans,
                                           string requestId,
                                           string UserName,
                                           int UserId);

        /// <summary>
        /// Save batch
        /// </summary>
        /// <param name="trans">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="clientId"></param>
        /// <param name="companyId"></param>
        /// <returns>Task of Batch </returns>

        Task<Batch> GetBatch(ITransactionHandler trans,
                                           int clientId,
                                           int companyId,
                                           string UserName);
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
                                        string RequestId,
                                        string UserName);


        /// <summary>
        /// Save batch item page
        /// </summary>
        /// <param name="transaction">  Instance of <see cref="ITransactionHandler"/></param>
        /// <param name="batchItemId"></param>
        /// <param name="fileName"></param>
        /// <param name="RequestId"></param>
        /// <param name="pageNo"></param>

        void SaveBatchItemPage(ITransactionHandler transaction,
                                       int batchItemId,
                                       int documentVersionId,
                                       string fileName,
                                       string originalFileName,
                                       string RequestId,
                                       string UserName,
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
        /// <param name="RequestId"></param>

        void SaveBatchItemMeta(ITransactionHandler transaction,
                                       List<BatchMetaDTO> batchMetaList,
                                       int batchId,
                                       int documentVersionId,
                                       string fileName,
                                       string batchItemRef,
                                       string RequestId,
                                       string UserName);
    }
}
