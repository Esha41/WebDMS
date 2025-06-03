using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class BopDictionaryDTO
    {
        public int Id { get; set; }
        [Required]
        public int DictionaryTypeId { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        public bool? IsActive { get; set; }
    }
}
