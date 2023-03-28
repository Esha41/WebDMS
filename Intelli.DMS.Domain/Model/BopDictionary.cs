using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class BopDictionary
    {
        public BopDictionary()
        {
            BatchItemFields = new HashSet<BatchItemField>();
            BatchMeta = new HashSet<BatchMetum>();
        }

        public int Id { get; set; }
        public int DictionaryTypeId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual DictionaryType DictionaryType { get; set; }
        public virtual ICollection<BatchItemField> BatchItemFields { get; set; }
        public virtual ICollection<BatchMetum> BatchMeta { get; set; }
    }
}
