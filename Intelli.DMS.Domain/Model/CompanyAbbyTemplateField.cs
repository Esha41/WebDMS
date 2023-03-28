using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class CompanyAbbyTemplateField
    {
        public int CompanyId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int? CompanyFlowId { get; set; }

        public virtual CompanyFlow CompanyFlow { get; set; }
    }
}
