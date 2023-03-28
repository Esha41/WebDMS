using Intelli.DMS.Api.DTO;
using Intelli.DMS.Auth.Api.Models;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.ActiveDirectory
{
    /// <summary>
    /// The active directory service.
    /// </summary>
    public interface IActiveDirectoryService
    {
        /// <summary>
        /// Are the user is in active directory.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>A bool.</returns>
        /// 
        bool IsUserInActiveDirectory(string email);
        /// <summary>
        /// Validates the active directory user.
        /// </summary>
        /// <param name="loginDTO">The login dto.</param>
        /// <returns>An UserLoginResult.</returns>
        Task<UserLoginResult> ValidateActiveDirectoryUser(LoginDTO loginDTO);
    }
}
