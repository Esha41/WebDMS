using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
     public partial class ClientCompanyCustomFieldValue
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int FieldId { get; set; }
        public int? DictionaryValueId { get; set; }
        public string RegisteredFieldValue { get; set; }
        public bool? IsUpdated { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public virtual Client Client { get; set; }
        public virtual BopDictionary DictionaryValue { get; set; }
        public virtual CompanyCustomField Field { get; set; }
    }
}
