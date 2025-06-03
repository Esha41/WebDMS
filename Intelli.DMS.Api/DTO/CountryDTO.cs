
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The country DTO.
    /// </summary>
    public class CountryDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// 
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        /// 
        [Required]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// 
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the 2 digit code.
        /// </summary>
        /// 
        [Required]
        public string Code2D { get; set; }

        /// <summary>
        /// Gets or sets the 3 digit code.
        /// </summary>
        /// 
        [Required]
        public string Code3D { get; set; }

        /// <summary>
        /// Gets or sets the mobile code.
        /// </summary>
        /// 
        [Required]
        public string MobileCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// 
        [Required]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// 
        [Required]
        public long UpdatedAt { get; set; }
    }
}
