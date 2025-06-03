using Intelli.DMS.Domain.Model;
using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class AlertDTO
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public string Msg { get; set; }
        public bool? IsRead { get; set; } = false;
        
    }
}
