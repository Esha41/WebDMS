using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class AdvancedSignatureCallHistory
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CallBodyInput { get; set; }
        public bool SigningSucceeded { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
    }
}
