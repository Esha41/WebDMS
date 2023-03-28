using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelli.DMS.Api.Services.DocumentApproved.Impl
{
    public class DocumentApprovedService : BaseService, IDocumentApprovedService
    {
        private readonly ILogger _logger;
        private readonly IRepository<DocumentApprovalHistory> _repositoryDocumentApprovalHistory;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<BatchItem> _repositoryBatchItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentApprovedService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/> will be injected</param>
        public DocumentApprovedService(DMSContext context,
            ILogger<DocumentApprovedService> logger,
            IHttpContextAccessor accessor) : base(accessor)
        {
            _repositoryDocumentApprovalHistory = new GenericRepository<DocumentApprovalHistory>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _logger = logger;
        }

        /// <summary>
        /// Check Document Approved By DocumentId
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> bool </returns>
        public bool checkDocumentApproved(int documentId)
        {
            try
            {
                GetDetails(documentId, out DocumentApprovalHistory checkDocumentApprovedPending, out List<int> UserRoles);

                if (checkDocumentApprovedPending == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Check Document Approved By Pending of DocumentClass Id
        /// </summary>
        /// <param name="documentClassId"></param>
        /// <returns> bool </returns>
        public bool checkDocumentApprovedByPendingofDocumentClassId(int documentClassId)
        {
            try
            {
                var batchItemReferenceList = _repositoryBatchItem.Query(x => x.DocumentClassId == documentClassId)
                                                                    .Select(x => x.BatchItemReference)
                                                                     .ToList();
                return _repositoryDocumentApprovalHistory.Query(x => batchItemReferenceList.Contains(x.BatchItemReference))
                                                     .Any(x => x.Approved == false);
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// check User RoleHave DocumentCheckout Access By DocumentId
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> bool </returns>
        public bool checkUserRoleHaveDocumentCheckoutAccess(int documentId)
        {
            try
            {
                GetDetails(documentId, out DocumentApprovalHistory checkDocumentApprovedPending, out List<int> UserRoles);
                if (checkDocumentApprovedPending == null)
                {
                    return false;
                }
                else if (UserRoles.Contains(checkDocumentApprovedPending.RoleId))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Get UserRole DocumentApproved History By DocumentId
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> int </returns>
        public int GetUserRoleDocumentApprovedHistory(int documentId)
        {
            try
            {
                List<DocumentApprovalHistory> checkDocumentApprovedPending;
                List<int> UserRoles;
                var batchItemRef = _repositoryBatchItem.Query(x => x.Id == documentId)
                                        .FirstOrDefault().BatchItemReference;

                checkDocumentApprovedPending = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItemRef && x.Approved == false)
                                                                                      .ToList();
                UserRoles = _repositorySystemUserRole.Query(x => x.SystemUserId == UserId)
                                                          .Select(x => x.SystemRoleId).ToList();
                foreach (var item in checkDocumentApprovedPending)
                {
                    foreach (var itemUserRole in UserRoles)
                    {
                        if (itemUserRole == item.RoleId)
                        {
                            return itemUserRole;
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }

        /// <summary>
        ///  Document Approval Status By BatchItemId
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <param name="statusFlag">out Parameter string statusFlag</param>
        /// <param name="StepNumber">out Parameter int StepNumber</param>
        public void DocumentApprovalStatus(int batchItemId, out string statusFlag, out int StepNumber)
        {
            string batchItemRef = _repositoryBatchItem.Query(x => x.Id == batchItemId)
                                       .FirstOrDefault().BatchItemReference;

            int checkDocumentApprovedHisListCount = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItemRef)
                                                                                   .ToList().Count;

            int checkDocumentApprovedListCount = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItemRef &&
                                                                                            x.Approved == true)
                                                                                   .ToList().Count;
            if ((checkDocumentApprovedHisListCount != checkDocumentApprovedListCount))
            {
                statusFlag = DocumentApprovedStatusConstants.Pending;
                StepNumber = checkDocumentApprovedListCount;
            }
            else
            {
                statusFlag = DocumentApprovedStatusConstants.Completed;
                StepNumber = checkDocumentApprovedListCount;
            }
        }

        /// <summary>
        ///  Get Details By DocumentId
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="checkDocumentApprovedPending">out parameter Object of DocumentApprovalHistory </param>
        /// <param name="UserRoles"> out parameter List of Integers </param>
        private void GetDetails(int documentId, out DocumentApprovalHistory checkDocumentApprovedPending, out List<int> UserRoles)
        {
            var batchItemRef = _repositoryBatchItem.Query(x => x.Id == documentId)
                                        .FirstOrDefault().BatchItemReference;

            checkDocumentApprovedPending = _repositoryDocumentApprovalHistory.Query(x => x.BatchItemReference == batchItemRef &&
                                                                                           x.Approved == false)
                                                                                  .FirstOrDefault();
            UserRoles = _repositorySystemUserRole.Query(x => x.SystemUserId == UserId)
                                                      .Select(x => x.SystemRoleId).ToList();
        }
    }
}
