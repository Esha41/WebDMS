using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentClassFieldBaseDTO
    {
        public int Id { get; set; }
        [Required]
        public int DocumentClassId { get; set; }
        [Required]
        public int DocumentClassFieldTypeId { get; set; }
        [Required]
        public string Uilabel { get; set; }
        [Required]
        public string DocumentClassFieldCode { get; set; }
        public int? UISort { get; set; }
        public int? DictionaryTypeId { get; set; }
        public bool? IsMandatory { get; set; }
        public bool? IsActive { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
    }

}
