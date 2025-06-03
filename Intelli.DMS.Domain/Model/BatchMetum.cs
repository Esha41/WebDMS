using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchMetum
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int DocumentVersionId { get; set; }
        public int DocumentClassFieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public string FieldValue { get; set; }
        public string FileName { get; set; }
        public string BatchItemReference { get; set; }

        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual DocumentVersion DocumentVersion { get; set; }
        public virtual BopDictionary DictionaryValue { get; set; }
        public virtual DocumentClassField DocumentClassField { get; set; }
    }
}
