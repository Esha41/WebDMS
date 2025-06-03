using Intelli.DMS.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.Views
{
    public class ClientRepositoryView : ICompanyEntity
    {
        public int Id { get; set; }
        public bool AppliedGDPR { get; set; }
        public int? ClientId { get; set; }
        public string ExternalId { get; set; }
        public string ClientName { get; set; }
        public string ClientStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RepositoryName { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CompanyId { get; set; }
    }
}
