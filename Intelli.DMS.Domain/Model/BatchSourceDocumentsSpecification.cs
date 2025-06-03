using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchSourceDocumentsSpecification
    {
        public int Id { get; set; }
        public int BatchSourceId { get; set; }
        public int DocumentClassId { get; set; }
        public string Description { get; set; }
        public bool IsVirtual { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual BatchSource BatchSource { get; set; }
        public virtual DocumentClass DocumentClass { get; set; }
    }
}
