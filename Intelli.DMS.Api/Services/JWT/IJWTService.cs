using Intelli.DMS.Api.Services.JWT.Impl;
using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The json web token service interface.
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Generates the JWT Token.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>A string.</returns>
        Task<TokenResponseDto> GenerateJWTToken(SystemUser userInfo , List<int> CompanyIds);
    }
}