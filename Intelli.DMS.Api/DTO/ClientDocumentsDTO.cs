using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class ClientDocumentsDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ExternalId { get; set; }
        public List<ClientCompanyFieldValuesDTO> ClientMetaData { get; set; }
        public List<FileContentDTO> Documents { get; set; } = new();
    }
}
