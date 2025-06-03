namespace Intelli.DMS.Api.DTO
{
    public class DocumentCheckoutListDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ClientName { get; set; }
        public int Version { get; set; }
        public int Status { get; set; }
        public string State { get; set; }
        public long CreatedOn { get; set; }
        public long LastModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public int VersionId { get; internal set; }
    }
}
