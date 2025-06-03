using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchSource
    {
        public BatchSource()
        {
            BatchSourceDocumentsSpecifications = new HashSet<BatchSourceDocumentsSpecification>();
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string BatchSourceName { get; set; }
        public string EnumValue { get; set; }
        public string BatchSourceCode { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<BatchSourceDocumentsSpecification> BatchSourceDocumentsSpecifications { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
    }
}
