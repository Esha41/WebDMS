using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The verify email DTO.
    /// </summary>
    public class BatchHistoryMetaDTO
    {
        /// <summary>
        /// Gets or sets the flag.
        /// Flag = 0 means dto contains code / token for email verification.
        /// Flag = 1 means dto contains code / token for password reset.
        /// </summary>
        [Required]
        public int Flag { get; set; }

        /// <summary>
        /// Gets or sets the AspNet user id.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
