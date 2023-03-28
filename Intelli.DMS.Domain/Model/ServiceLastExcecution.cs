using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class ServiceLastExcecution
    {
        public string ServiceName { get; set; }
        public DateTime Time { get; set; }
    }
}
