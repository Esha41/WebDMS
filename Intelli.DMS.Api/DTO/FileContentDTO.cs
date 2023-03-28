using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class FileContentDTO
    {
            public string DocumentClassCode { get; set; }
            public List<FileDTO> Files { get; set; }
            public List<FileContentMetaDataDTO> Data { get; set; }
    }
}
