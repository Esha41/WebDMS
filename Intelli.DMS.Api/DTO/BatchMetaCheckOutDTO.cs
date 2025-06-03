using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class BatchMetaCheckOutDTO:BatchMetaBaseDTO
    {
        public int Id { get; set; }
        [Required]
        public int BatchId { get; set; }
    }
}
