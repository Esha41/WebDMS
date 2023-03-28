using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class RulesXref
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DocClassFieldId { get; set; }
        public byte RuleType { get; set; }
        public int? Value { get; set; }
        public string Formula { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
