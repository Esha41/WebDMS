using Intelli.DMS.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentReviewDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }  // from batch Item page
        [Required]
        public string ClientName { get; set; }   // from batch
        [Required]
        public int Version { get; set; }  //?
        public int State { get; set; }  //?
        public string Status{ get; set; }  //?

        public bool? IsActive { get; set; }
        [Required]
        public long CreatedAt { get; set; }
        [Required]
        public long UpdatedAt { get; set; }

    }
}
