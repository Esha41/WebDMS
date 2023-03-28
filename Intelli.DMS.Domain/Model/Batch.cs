using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Batch
    {
        public Batch()
        {
            BatchItems = new HashSet<BatchItem>();
            BatchMeta = new HashSet<BatchMetum>();
            BatchMetaHistories = new HashSet<BatchMetaHistory>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? BusinessUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BatchStatusId { get; set; }
        public int? LockedBy { get; set; }
        public DateTime? VerifiedStartDate { get; set; }
        public DateTime? VerifiedEndDate { get; set; }
        public int BatchSourceId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? RetriesCount { get; set; }
        public string MandatoryAlerts { get; set; }
        public string ValidationAlerts { get; set; }
        public int? CurrentOtp { get; set; }
        public DateTime? OtpvalidUntil { get; set; }
        public bool? AppliedGdpr { get; set; }
        public DateTime? RecognizedDate { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public Guid? InternalRequestId { get; set; }
        public string LockedByNavigationId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int? CustomerId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual BatchSource BatchSource { get; set; }
        public virtual BatchStatus BatchStatus { get; set; }
        public virtual Bu BusinessUnit { get; set; }
        public virtual Client Customer { get; set; }
        public virtual ICollection<BatchItem> BatchItems { get; set; }
        public virtual ICollection<BatchMetum> BatchMeta { get; set; }
        public virtual ICollection<BatchMetaHistory> BatchMetaHistories { get; set; }
    }
}
