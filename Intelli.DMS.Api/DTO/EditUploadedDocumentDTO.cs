namespace Intelli.DMS.Api.DTO
{
    public class EditUploadedDocumentDTO
    {
        public string BatchMetaData { get; set; }
        public string DocumentName { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public int DocumentId { get; set; }
        public int CompanyId { get; set; }

    }
}
