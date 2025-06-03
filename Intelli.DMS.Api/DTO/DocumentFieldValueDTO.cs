namespace Intelli.DMS.Api.DTO
{
    public class DocumentFieldValueDTO: DocumentClassFieldDTO
    {
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public string FieldValue { get; set; }

    }
}
