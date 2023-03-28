using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The screens DTO.
    /// </summary>
    public class ScreensDTO
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ScreenName.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ScreenName { get; set; }
    }
}
