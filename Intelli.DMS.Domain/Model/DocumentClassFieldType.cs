using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentClassFieldType
    {
        public DocumentClassFieldType()
        {
            DocumentClassFields = new HashSet<DocumentClassField>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<DocumentClassField> DocumentClassFields { get; set; }
    }
}
