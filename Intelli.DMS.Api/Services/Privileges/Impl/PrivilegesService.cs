using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The privileges service.
    /// </summary>
    public class PrivilegesService : IPrivilegesService
    {
        private readonly DMSContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PrivilegesService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivilegesService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The auto mapper.</param>
        /// <param name="logger">The logger.</param>
        public PrivilegesService(DMSContext context, IMapper mapper, ILogger<PrivilegesService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets the user privileges DTO.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An UserReadPrivilegesDTO object.</returns>
        public async Task<UserReadPrivilegesDTO> GetUserPrivilegesAsync(int userId)
        {
            try
            {
                var user = await _context.SystemUsers.Where(u => u.Id == userId)
                    .Include(ur => ur.SystemUserRoles)
                    .ThenInclude(urr => urr.SystemRole)
                    .ThenInclude(urs => urs.RoleScreens)
                    .ThenInclude(rs => rs.Screen)
                    .Include(ur => ur.SystemUserRoles)
                    .ThenInclude(urr => urr.SystemRole)
                    .ThenInclude(r => r.RoleScreenElements)
                    .ThenInclude(rse => rse.ScreenElement)
                    .Include(ur => ur.UserPreferences)
                    .FirstOrDefaultAsync();

                var dto = new PrivilegesDTOFactory(_mapper).Build(user);

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get privileges / preferences for UserId: {0}", userId);
            }
            return new UserReadPrivilegesDTO { Id = userId };
        }

        public List<RoleDTO> GetUserRoles(int userId)
        {
            try
            {
                var user =  _context.SystemUsers.Where(u => u.Id == userId)
                    .Include(ur => ur.SystemUserRoles)
                    .ThenInclude(urr => urr.SystemRole)
                    .FirstOrDefaultAsync();
                var roleList = user.Result.SystemUserRoles.Select(x => x.SystemRole).ToList();
                roleList.ForEach(x => x.Company = null);
                var dto = _mapper.Map<List<RoleDTO>>(roleList);

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get User Roles for UserId: {0}", userId);
            }
            return new List<RoleDTO>();
        }
    }
}
