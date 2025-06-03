namespace Intelli.DMS.Api.DTO
{
    public class DocumentReviewPaginationDTO
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public string DocumentClassName { get; set; }

        public string DocumentTypeName { get; set; }

        public string ClientName { get; set; }

        public string LastModifiedBy { get; set; }
        public bool IsCheckedOut { get; set; }
        public string LastModifiedById { get; set; }

        public bool IsLastModifiedByYou { get; set; }
        public string CheckedOutBy { get; set; }
        public int BatchId { get; set; }
        public string DocumentState { get; set; }
        public int Status { get; set; }
        public long CreatedAt { get; set; }

        public long UpdatedAt { get; set; }
    }
}
