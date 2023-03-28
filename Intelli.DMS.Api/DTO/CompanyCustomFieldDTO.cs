namespace Intelli.DMS.Api.DTO
{
    public class CompanyCustomFieldDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int FieldTypeId { get; set; }
        public string Uilabel { get; set; }
        public int? UISort { get; set; }
        public int? DictionaryTypeId { get; set; }
        public bool? IsMandatory { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string RecordStatus { get; set; }
        public int? DictionaryValueId { get; set; }
        public string FieldValue { get; set; }
        public virtual DictionaryTypeDTO DictionaryType { get; set; }
        public virtual CompanyDTO Company { get; set; }
        public virtual DocumentClassFieldTypeDTO DocumentClassFieldType { get; set; }
    }
}
