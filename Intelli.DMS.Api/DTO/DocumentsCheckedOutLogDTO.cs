using Intelli.DMS.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentsCheckedOutLogDTO
    {
        /// </summary>
        ///  [Required]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        [Required]
        public int BatchItemId { get; set; }

        [Required]
        public int SystemUserId { get; set; }

        [Required]
        public string Action { get; set; }

        public long CheckedOutAt { get; set; }
        public long CheckedInAt { get; set; }
    }
}
