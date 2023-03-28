using AutoMapper;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The custom password validator.
    /// </summary>
    public class DMSPasswordValidator : PasswordValidator<AspNetUser>
    {
        private readonly DMSContext _context;
        private readonly int RestrictLastUsedPasswords;

        /// <summary>
        /// Initializes a new instance of the <see cref="DMSPasswordValidator"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The auto mapper.</param>
        public DMSPasswordValidator(DMSContext context, IMapper mapper)
        {
            _context = context;

            var dto = ConfigurationHelper.Read(context, mapper);
            RestrictLastUsedPasswords = dto.RestrictLastUsedPasswords;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task.</returns>
        public override async Task<IdentityResult> ValidateAsync(UserManager<AspNetUser> manager, AspNetUser user, string password)
        {
            if (RestrictLastUsedPasswords > 0)
            {
                // Cannot use last password(s)
                var query = _context.PasswordHistories.AsQueryable()
                                        .Where(x => x.SystemUserId == user.SystemUserId)
                                        .OrderByDescending(x => x.Id)
                                        .Take(RestrictLastUsedPasswords);

                var list = await query.ToListAsync();

                if (list.Count > 0)
                {
                    string passwordHash = await StringHasher.GetHashAsync(password);

                    if (list.Exists(x => x.PasswordHash == passwordHash))
                        return await Task.FromResult(IdentityResult.Failed(new IdentityError
                        {
                            Code = "OldPassword",
                            Description = $"{RestrictLastUsedPasswords}"
                        }));
                }
            }

            // If all validations passed return success
            return await Task.FromResult(IdentityResult.Success);
        }
    }
}
