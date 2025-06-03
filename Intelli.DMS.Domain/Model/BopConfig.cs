using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BopConfig
    {
        public int Id { get; set; }
        public string Setting { get; set; }
        public string EnumValue { get; set; }
        public string Value { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
