using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentCheckOut
{
    public interface ICheckOutService
    {
        /// <summary>
        /// Checked out the document.
        /// </summary>
        /// <param name="batchItemId">The batchItemId representing a document.</param>
        /// <returns>An checkedOutDto.</returns>
        Task<DocumentsCheckedOutDTO> CheckOutDocument(int batchItemId);

        /// <summary>
        /// Check if the document is already checked out.
        /// </summary>
        /// <param name="batchItemId">The batchItemId representing a document.</param>
        /// <returns>Integer value as DocumentsCheckedOut Id.</returns>
        int IsDocumentCheckOut(int batchItemId);
    }
}
