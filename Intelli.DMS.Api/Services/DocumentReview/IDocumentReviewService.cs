using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Model;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Services.DocumentReview
{
    public interface IDocumentReviewService
    {
        // <summary>
        /// Get All documents of pending review and which are not checked out yet.
        /// </summary>
        /// <returns>List of documents to be reviews</returns>
        List<DocumentReviewDTO> GetPendingReviewDoc();
    }
}
