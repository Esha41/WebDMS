using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The document class DTO.
    /// </summary>
    public class DocumentClassContractDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        /// 
        public string DocumentClassCode { get; set; }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// Gets or sets the Batch Meta List.
        /// </summary>
        public List<BatchMetaDTO> BatchMetaList { get; set; }

        /// <summary>
        /// Gets or sets the DocumentClassFields.
        /// </summary>
        public List<DocumentClassFieldContractDTO> DocumentClassFields { get; set; }
    }
}