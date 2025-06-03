using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    /// <summary>
    /// The asp net user.
    /// </summary>
    public partial class AspNetUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the system user id.
        /// </summary>
        public int SystemUserId { get; set; }

        /// <summary>
        /// Gets or sets the system user.
        /// </summary>
        public SystemUser SystemUser { get; set; }
    }
}
