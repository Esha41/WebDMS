using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.OLDDBModel
{
    public partial class FilingCategory
    {
        public int FilingCategoryId { get; set; }
        public string FilingCategoryDescription { get; set; }
        public bool FilingCategoryForEurobankTrader { get; set; }
    }
}
