using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class UpdatedFieldDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string EnumValue { get; set; }
    }
}
