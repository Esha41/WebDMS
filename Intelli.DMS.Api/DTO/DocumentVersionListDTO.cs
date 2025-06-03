namespace Intelli.DMS.Api.DTO
{
    public class DocumentVersionListDTO
    {
        public int ID { get; set; }
        public int BatchItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public int Version { get; set; }
        public int VersionId { get; set; }
        public string LastModifiedBy { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
