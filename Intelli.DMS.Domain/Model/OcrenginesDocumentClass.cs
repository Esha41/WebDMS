using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class OcrenginesDocumentClass
    {
        public int Id { get; set; }
        public int DocumentClassId { get; set; }
        public int OcrengineId { get; set; }
        public string OcrengineDocumentClassCode { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual DocumentClass DocumentClass { get; set; }
        public virtual Ocrengine Ocrengine { get; set; }
    }
}
