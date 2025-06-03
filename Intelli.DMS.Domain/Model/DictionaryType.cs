using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class DictionaryType
    {
        public DictionaryType()
        {
            BopDictionaries = new HashSet<BopDictionary>();
            DocumentClassFields = new HashSet<DocumentClassField>();
        }

        public int Id { get; set; }
        public string DictionaryTypeName { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<BopDictionary> BopDictionaries { get; set; }
        public virtual ICollection<DocumentClassField> DocumentClassFields { get; set; }
    }
}
