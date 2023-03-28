using Intelli.DMS.Domain.Model;
using System.Collections.Generic;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentClassFieldDTO : DocumentClassFieldBaseDTO
    {
        public string recordStatus { get; set; }
        public bool IsModifiedable { get; set; }
        public bool IsDeleteable { get; set; }
        public virtual DictionaryTypeDTO DictionaryType { get; set; }
        public virtual DocumentClassFieldTypeDTO DocumentClassFieldType { get; set; }
    }
}
