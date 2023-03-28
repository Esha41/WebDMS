using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentRejectionReason
    {
        public DocumentRejectionReason()
        {
            DocumentRejectionReasonCompanies = new HashSet<DocumentRejectionReasonCompany>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Descr { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<DocumentRejectionReasonCompany> DocumentRejectionReasonCompanies { get; set; }
    }
}
