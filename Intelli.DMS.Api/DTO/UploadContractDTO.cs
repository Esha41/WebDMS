using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class UploadContractDTO
    {
        public ClientContractDTO ClientInfo { get; set; }
        public List<DocumentClassContractDTO> DocumentClasses{ get; set; }
    }
}
