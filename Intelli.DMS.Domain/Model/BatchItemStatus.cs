using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchItemStatus
    {
        public BatchItemStatus()
        {
            BatchItems = new HashSet<BatchItem>();
        }

        public int Id { get; set; }
        public string BatchItemStatusName { get; set; }
        public string EnumValue { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<BatchItem> BatchItems { get; set; }
    }
}
