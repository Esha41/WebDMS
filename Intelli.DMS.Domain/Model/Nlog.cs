using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Nlog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string RequestId { get; set; }
        public int? PosId { get; set; }
        public string Level { get; set; }
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string Exception { get; set; }
        public string Data { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int LoggedOn { get; set; }
    }
}
