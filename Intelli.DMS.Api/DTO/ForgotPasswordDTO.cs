
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The forgot password DTO.
    /// </summary>
    public class ForgotPasswordDTO
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
