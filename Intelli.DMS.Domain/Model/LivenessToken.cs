using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class LivenessToken
    {
        public int Id { get; set; }
        public string Gr { get; set; }
        public string En { get; set; }
        public string NumberResult { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
