using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Ocrengine
    {
        public Ocrengine()
        {
            OcrenginesDocumentClasses = new HashSet<OcrenginesDocumentClass>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<OcrenginesDocumentClass> OcrenginesDocumentClasses { get; set; }
    }
}
