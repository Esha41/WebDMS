using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class ClientDocumentsOldDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ExternalId { get; set; }
        public List<ClientCompanyFieldValuesDTO> ClientMetaData { get; set; }
        public List<FileContentOldDTO> Documents { get; set; } = new();
    }
}
