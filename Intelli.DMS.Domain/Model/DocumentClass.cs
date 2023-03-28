using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentClass
    {
        public DocumentClass()
        {
            BatchItems = new HashSet<BatchItem>();
            BatchSourceDocumentsSpecifications = new HashSet<BatchSourceDocumentsSpecification>();
            DocumentClassFields = new HashSet<DocumentClassField>();
            DocumentsPerCompanies = new HashSet<DocumentsPerCompany>();
            OcrenginesDocumentClasses = new HashSet<OcrenginesDocumentClass>();
        }

        public int Id { get; set; }
        public string DocumentClassName { get; set; }
        public string DocumentClassCode { get; set; }
        public int DocumentTypeId { get; set; }
        public int CompanyId { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<BatchItem> BatchItems { get; set; }
        public virtual ICollection<BatchSourceDocumentsSpecification> BatchSourceDocumentsSpecifications { get; set; }
        public virtual ICollection<DocumentClassField> DocumentClassFields { get; set; }
        public virtual ICollection<DocumentsPerCompany> DocumentsPerCompanies { get; set; }
        public virtual ICollection<OcrenginesDocumentClass> OcrenginesDocumentClasses { get; set; }
    }
}
