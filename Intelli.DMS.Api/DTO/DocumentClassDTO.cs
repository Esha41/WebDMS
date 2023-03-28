using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The document class DTO.
    /// </summary>
    public class DocumentClassDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// 
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document class name.
        /// </summary>
        [Required]
        public string DocumentClassName { get; set; }

        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        /// 
        [Required]
        public string DocumentClassCode { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeId.
        /// </summary>
        /// 
        [Required]
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

        public DocumentTypeDTO DocumentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active or not.
        /// </summary>
        public bool? IsActive { get; set; }
        public bool IsDocumentTypeModifiedable { get; set; }
        public bool IsDocumentClassNameDuplicationCheck { get; set; }
        public bool IsDocumentClassCodeDuplicationCheck { get; set; }
        public int previousDocumentTypeId { get; set; }

        public List<DocumentClassFieldDTO> Fields { get; set; }
        public List<DocumentClassFieldCodeDTO> FieldCodes { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public int previousCompanyId { get; set; }
        
        /// <summary>
        /// Overriding the ToString to generate string representation of the object
        /// Mainly to be used to logging.
        /// </summary>
        /// <returns>JSON string of teh current object.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}