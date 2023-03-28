using System;

namespace Intelli.DMS.Api.DTO
{
    public class ClientDocumentDetailDTO
    {
        public int DocumentId { get; set; }
        public DateTime DocumentInsertDate { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryDescription { get; set; }
        public string MasterDocumentCategoryDescription { get; set; }
    }
}
