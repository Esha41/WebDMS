using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentsCheckedOutDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Required]
        public int Id { get; set; }
        [Required]
        public int BatchItemId { get; set; }
        [Required]
        public int SystemUserId { get; set; }
        public bool? IsActive { get; set; }

    }
}
