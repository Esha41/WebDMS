using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentClassFieldTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public bool? IsActive { get; set; }
    }
}
