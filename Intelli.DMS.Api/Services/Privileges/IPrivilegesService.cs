
using Intelli.DMS.Api.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The privileges service.
    /// </summary>
    public interface IPrivilegesService
    {
        /// <summary>
        /// Gets the user privileges DTO async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An UserReadPrivilegesDTO object.</returns>
        Task<UserReadPrivilegesDTO> GetUserPrivilegesAsync(int userId);
        /// <summary>
        /// Gets the user Roles DTO async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An List of  RolesDTO object.</returns>
        List<RoleDTO> GetUserRoles(int id);
    }
}