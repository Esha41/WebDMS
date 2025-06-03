using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            DocumentClasses = new HashSet<DocumentClass>();
        }

        public int Id { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeCode { get; set; }
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<DocumentClass> DocumentClasses { get; set; }
    }
}
