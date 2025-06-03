using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class CompanyRepositoryDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
