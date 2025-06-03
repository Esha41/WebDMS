using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class CompanySigningDocument
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
