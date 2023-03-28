namespace Intelli.DMS.Api.Services.Status
{
    public interface IStatusService
    {

        /// <summary>
        /// Set Batch Item Status
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <param name="batchItemStatusId"></param>
        /// <param name="requestId"></param>
        void SetBatchItemStatus(int batchItemId, int batchItemStatusId, string requestId,
                                           string userName);

        /// <summary>
        /// Check And Replace Batch Status
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <param name="batchStatusId"></param>
        /// <returns>  </returns>
        void SetBatchStatusWithBatchItemId(int batchItemId, int batchStatusId, string requestId);

        /// <summary>
        /// Set BatchStatus With ClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="batchStatusId"></param>
        /// <returns>  </returns>
        void SetBatchStatusWithClientId(int clientId, int batchStatusId);

    }
}
