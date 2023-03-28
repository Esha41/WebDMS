using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DictionaryTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string DictionaryTypeName { get; set; }
        public string SelectedDictionaryTypeName { get; set; }
        public virtual ICollection<BopDictionaryDTO> BopDictionaries { get; set; }
    }
}
