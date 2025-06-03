using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BatchItemPage
    {
        public int Id { get; set; }
        public int BatchItemId { get; set; }
        public int DocumentVersionId { get; set; }
        public string FileName { get; set; }

        public int Number { get; set; }
        public string OriginalName { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual BatchItem BatchItem { get; set; }
        public virtual DocumentVersion DocumentVersion { get; set; }
    }
}
