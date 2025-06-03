using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class CompanyAbbyTemplate
    {
        public int CompanyId { get; set; }
        public int DocumentClassId { get; set; }

        public virtual Company Company { get; set; }
        public virtual DocumentClass DocumentClass { get; set; }
    }
}
