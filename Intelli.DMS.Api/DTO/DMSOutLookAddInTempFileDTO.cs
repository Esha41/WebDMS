namespace Intelli.DMS.Api.DTO
{
    public class DMSOutLookAddInTempFileDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FilePath { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }
    }
}
