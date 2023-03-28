using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The document class DTO.
    /// </summary>
    public class DocumentClassContractsDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document class name.
        /// </summary>
        public string DocumentClassName { get; set; }

        /// <summary>
        /// Gets or sets the CompanyId.
        /// </summary>
        /// 
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        /// 
        public string DocumentClassCode { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeId.
        /// </summary>
        /// 
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentClassFields.
        /// </summary>
        public List<DocumentClassFieldContractDTO> DocumentClassFields { get; set; }
    }
}