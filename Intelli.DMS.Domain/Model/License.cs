using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class License
    {
        public string LicenseName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Comments { get; set; }
    }
}
