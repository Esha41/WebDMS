using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class BatchMetaBaseDTO
    {
        [Required]
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        [Required]
        public string FieldValue { get; set; }
    }
}
