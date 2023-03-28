using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class ClientView
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public bool IsNotValidForTransaction { get; set; }
        public string Reason { get; set; }
        public int GdprdaysToBeKept { get; set; }
        public string ExternalId { get; set; }
        public string ClientStatus { get; set; }
    }
}
