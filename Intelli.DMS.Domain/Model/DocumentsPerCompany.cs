using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentsPerCompany
    {
        public int Id { get; set; }
        public int DocumentClassId { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual DocumentClass DocumentClass { get; set; }
    }
}
