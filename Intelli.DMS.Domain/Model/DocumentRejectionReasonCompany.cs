using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentRejectionReasonCompany
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DocumentRejectionReasonId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual DocumentRejectionReason DocumentRejectionReason { get; set; }
    }
}
