using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentCheckIn
{
    public interface ICheckInService
    {
        /// <summary>
        /// Checked in a document.
        /// </summary>
        /// <param name="batchItemId">batchItemId relevant to document.</param>
        /// <param name="docCheckOutId">docCheckOutId to be deleted.</param>
        /// <returns>boolean if document is checked In.</returns>
        Task<bool> CheckInDocument(int batchItemId, int docCheckOutId);

        /// <summary>
        /// Check if document check in is allowed by the current user.
        /// </summary>
        /// <param name="batchItemId">The batchItemId representing a document.</param>
        /// <param name="UserId">The current userId.</param>
        /// <returns>Integer value as DocumentsCheckedOut Id.</returns>
        int IsCheckInAllowed(int batchItemId, int userId);
    }
}
