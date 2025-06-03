using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchItem
    {
        public BatchItem()
        {
            BatchItemFields = new HashSet<BatchItemField>();
            BatchItemPages = new HashSet<BatchItemPage>();
            InverseParent = new HashSet<BatchItem>();
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public int DocumentVersionId { get; set; }
        public int BatchItemStatusId { get; set; }
        public int? DocumentClassId { get; set; }
        public string FileName { get; set; }
        public string BatchItemReference { get; set; }
        public DateTime OccuredAt { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CompanyId { get; set; }
        public int? SystemRoleId { get; set; }

        public virtual SystemRole SystemRole { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual BatchItemStatus BatchItemStatus { get; set; }
        public virtual DocumentVersion DocumentVersion { get; set; }
        public virtual DocumentClass DocumentClass { get; set; }
        public virtual BatchItem Parent { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<BatchItemPage> BatchItemPages { get; set; }
        public virtual ICollection<BatchItem> InverseParent { get; set; }
    }
    public partial class BatchItem : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
