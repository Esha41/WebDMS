using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The documents per company DTO.
    /// </summary>
    public class DocumentsPerCompanyDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// 
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the document class id.
        /// </summary>
        /// 
        [Required]
        public int DocumentClassId { get; set; }

        /// <summary>
        /// Gets or sets the document group id.
        /// </summary>
        /// 
        [Required]
        public int DocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        /// 
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// 
        [Required]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// 
        [Required]
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the document class.
        /// </summary>
        public virtual DocumentClassDTO DocumentClass { get; set; }
    }
}