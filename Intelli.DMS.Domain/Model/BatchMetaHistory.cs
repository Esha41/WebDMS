using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchMetaHistory
    {
        public int Id { get; set; }
        public DateTime OccuredAt { get; set; }
        public int BatchId { get; set; }
        public string PreviousValues { get; set; }
        public string CurrentValues { get; set; }
        public int? SystemUserId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
