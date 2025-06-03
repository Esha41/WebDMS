namespace Intelli.DMS.Api.DTO
{
    public class ContractSearchViewDTO
    {
        public int Id { get; set; }

        public int RepositoryName { get; set; }
        public string ClientName { get; set; }
        public string DocumentClassName { get; set; }
        public string DocumentTypeName { get; set; }
        public string ContractMetaData { get; set; }
        public string ClientMetaData { get; set; }
        public string BoxNumber { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
