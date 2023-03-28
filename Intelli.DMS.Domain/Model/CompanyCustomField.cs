using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class CompanyCustomField
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DocumentClassFieldTypeId { get; set; }
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
        public virtual Company Company { get; set; }
        public virtual DocumentClassFieldType DocumentClassFieldType { get; set; }
    }
    public partial class CompanyCustomField : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
