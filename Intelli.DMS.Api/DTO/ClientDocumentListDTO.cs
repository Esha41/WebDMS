namespace Intelli.DMS.Api.DTO
{
    public class ClientDocumentListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string DocumentClassName { get; set; }
        public long StatusTime { get; set; }
        public long LastModifiedOn { get; set; }
        public bool IsCheckOutDocument { get; set; }
        public string CheckoutBy { get; set; }
        public string State { get; set; }
    }
}
