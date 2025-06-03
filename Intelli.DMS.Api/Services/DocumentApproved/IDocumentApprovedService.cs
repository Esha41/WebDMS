namespace Intelli.DMS.Api.Services.DocumentApproved
{
    public interface IDocumentApprovedService
    {
        /// <summary>
        /// Check Document Approved
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> Boolean </returns>
        bool checkDocumentApproved(int documentId);

        /// <summary>
        /// Document Approval Status
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <returns> statusFlag,StepNumber </returns>
        void DocumentApprovalStatus(int batchItemId, out string statusFlag, out int stepNumber);

        /// <summary>
        /// check UserRole Have Document Checkout Access
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> Boolean </returns>
        bool checkUserRoleHaveDocumentCheckoutAccess(int documentId);

        int GetUserRoleDocumentApprovedHistory(int documentId);

        /// <summary>
        /// check All Documents Approved of This documentClassId
        /// </summary>
        /// <param name="documentClassId"></param>
        /// <returns> Boolean </returns>
        bool checkDocumentApprovedByPendingofDocumentClassId(int documentClassId);
    }
}
