using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchStatus
    {
        public BatchStatus()
        {
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string BatchStatusName { get; set; }
        public string EnumValue { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}
