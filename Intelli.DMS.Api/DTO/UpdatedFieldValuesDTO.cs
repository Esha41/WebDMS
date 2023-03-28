using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class UpdatedFieldValuesDTO
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public int DocumentClassFieldId { get; set; }
        [Required]
        public string FieldValue { get; set; }
        public UpdatedFieldDTO DocumentClassField { get; set; }
    }
}
