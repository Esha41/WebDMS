using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentClassField
    {
        public DocumentClassField()
        {
            BatchItemFields = new HashSet<BatchItemField>();
            BatchMeta = new HashSet<BatchMetum>();
        }

        public int Id { get; set; }
        public int DocumentClassId { get; set; }
        public int DocumentClassFieldTypeId { get; set; }
        public string DocumentClassFieldCode { get; set; }
        public string Uilabel { get; set; }
        public int? UISort { get; set; }
        public int? DictionaryTypeId { get; set; }
        public bool? IsMandatory { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }

        public virtual DictionaryType DictionaryType { get; set; }
        public virtual DocumentClass DocumentClass { get; set; }
        public virtual DocumentClassFieldType DocumentClassFieldType { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<BatchMetum> BatchMeta { get; set; }
    }
}
