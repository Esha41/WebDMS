using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentDelete
{
    public interface IDocumentDeleteService
    {
        /// <summary>
        /// delete document and also delete its corresponding data
        /// </summary>
        /// <param name="documentId"></param>
        Task DeleteDocumentWithAssociatedData(int batchItemId);
    }
}