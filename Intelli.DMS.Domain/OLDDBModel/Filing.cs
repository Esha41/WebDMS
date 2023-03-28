using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.OLDDBModel
{
    public partial class Filing
    {
        public int FilingId { get; set; }
        public decimal? FilingXacode { get; set; }
        public string FilingPathname { get; set; }
        public int? FilingCategory { get; set; }
        public DateTime? FilingInsertDate { get; set; }
        public decimal? FilingUserInsert { get; set; }
        public bool? FilingIsActive { get; set; }
        public string FilingFolder { get; set; }
    }
}
