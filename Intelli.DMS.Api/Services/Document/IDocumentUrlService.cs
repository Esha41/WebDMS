using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The Document Url service .
    /// </summary>
    public interface IDocumentUrlService
    {
        /// <summary>
        /// Get Document Url.
        /// </summary>
        /// <param name="batchItemId">The batch item id .</param>
        /// <returns>A Task string.</returns>
        Task<string> GetDocumentUrlByBatchItemId(int batchItemId);
    }
}
