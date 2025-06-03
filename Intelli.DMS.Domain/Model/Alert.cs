namespace Intelli.DMS.Domain.Model
{
    public partial class Alert
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public string Msg { get; set; }
        public bool? IsRead { get; set; } = false;
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }
    }
}
