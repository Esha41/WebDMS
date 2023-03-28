using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncryptionHelper = Intelli.DMS.Api.Helpers.EncryptionHelper;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICompanyRepository<SystemRole> _rolesRepository;
        private readonly IPrivilegesService _privilegesService;
        private readonly IRepository<Screen> _screensRepository;
        private readonly IRepository<RoleScreen> _roleScreensRepository;
        private readonly IRepository<RoleScreenElement> _roleScreenElementsRepository;
        private readonly IRepository<SystemUser> _systemUserRepository;
        private readonly IRepository<SystemUserRole> _systemUserRoleRepository;
        private readonly IRepository<Company> _companyRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public RolesController(DMSContext context, IMapper mapper,
            IPrivilegesService privilegesService,
            ILogger<RolesController> logger,
            IEventSender sender)
        {
            _rolesRepository = new CompanyEntityRepository<SystemRole>(context);
            _screensRepository = new GenericRepository<Screen>(context);
            _roleScreensRepository = new GenericRepository<RoleScreen>(context);
            _roleScreenElementsRepository = new GenericRepository<RoleScreenElement>(context);
            _systemUserRepository = new GenericRepository<SystemUser>(context);
            _systemUserRoleRepository = new GenericRepository<SystemUserRole>(context);
            _companyRepository = new GenericRepository<Company>(context);

            ((GenericRepository<Screen>)_screensRepository).AfterSave =
            ((GenericRepository<RoleScreen>)_roleScreensRepository).AfterSave =
            ((GenericRepository<RoleScreenElement>)_roleScreenElementsRepository).AfterSave =
            ((GenericRepository<SystemUser>)_systemUserRepository).AfterSave =
            ((GenericRepository<SystemUserRole>)_systemUserRoleRepository).AfterSave =
            ((CompanyEntityRepository<SystemRole>)_rolesRepository).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _privilegesService = privilegesService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of IEnumerable of Role</returns>
        [HttpGet]
        public ActionResult<IEnumerable<RoleDTO>> GetAll([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("GetRoles Called with params: {0}", queryStringParams);
            PagedResult<RoleDTO> result = null;
            try
            {
                QueryResult<SystemRole> queryResult = _rolesRepository.Get(CompanyIds,
                        queryStringParams.FilterExpression,
                        queryStringParams.OrderBy,
                        queryStringParams.PageSize,
                        queryStringParams.PageNumber, x => x.Company);

                int total = queryResult.Count;
                int re = 0;
                IEnumerable<SystemRole> roleList = queryResult.List;
                var companyList = roleList.Select(x => x.Company).ToList();
                foreach (var item in companyList)
                {
                    if (!int.TryParse(item.UsersPerCompany,out re))
                    {
                       item.UsersPerCompany = EncryptionHelper.DecryptString(item.UsersPerCompany);
                    }
                }
                result = new PagedResult<RoleDTO>(
                        total,
                        queryStringParams.PageNumber,
                        roleList.Select(x => _mapper.Map<RoleDTO>(x)).ToList(),
                        queryStringParams.PageSize
                    );
            }
            catch (ArgumentException e)
            {
                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <returns>Action Result of IEnumerable of Role.</returns>
        [HttpGet]
        [Route("RolesCurrentUser")]
        public IActionResult GetRolesOfCurrentUser()
        {
            try
            {
                var dto = _privilegesService.GetUserPrivilegesAsync(UserId);
                return Ok(new { Items = dto.Result.ScreenPriviliges });
            }
            catch(Exception ex){
                // Log error message
                _logger.LogError("{0}: {1}", ex.Message, ex);

                return BadRequest(new
                {
                    Errors = ex,
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Gets all active.
        /// </summary>
        /// <returns>Action Result of IEnumerable of Role.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetActive()
        {
            var dto = new RoleDTO();


            var result = await _rolesRepository.GetAllActiveAsync(CompanyIds, nameof(dto.Priority),x=>x.Company);

            // If user is associated to any company then filter out the roles of those companies on the basis of priority
            // If user is super admin this check will not run and all data will be returned
            if (CompanyIds.Count > 0)
            {
                var userRoles = _systemUserRepository.Query(x => x.Email == UserName).Select(x => x.Id);
                var systemUserRoles = _systemUserRoleRepository.Query(x => x.SystemUserId == userRoles.FirstOrDefault())
                                                               .Include(x => x.SystemRole)
                                                                .ThenInclude(x=>x.Company)
                                                               .OrderBy(x => x.SystemRole.Priority)
                                                               .ToList().FirstOrDefault();


               
                var filteredResult = result.List.FindAll(x => x.Priority >= systemUserRoles.SystemRole.Priority).ToList();

                filteredResult.ForEach(x => x.Name = $"{x.Company.CompanyName} - {x.Name}");
                int checkUserPerCompany = 0;
                foreach (var item in filteredResult)
                {
                    if (!int.TryParse(item?.Company?.UsersPerCompany, out checkUserPerCompany))
                        item.Company.UsersPerCompany = EncryptionHelper.DecryptString(item.Company.UsersPerCompany);
                }
                return Ok(new { Items = filteredResult.Select(x => _mapper.Map<RoleDTO>(x)).ToList() });
            }
            result.List.ForEach(x => x.Name = $"{x.Company.CompanyName} - {x.Name}");
            int check = 0;
            foreach (var item in result.List)
            {
                if(!int.TryParse(item?.Company?.UsersPerCompany,out check))
                item.Company.UsersPerCompany = EncryptionHelper.DecryptString(item.Company.UsersPerCompany);
            }
            return Ok(new { Items = result.List.Select(x => _mapper.Map<RoleDTO>(x)).ToList() });
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task: ActionResult of Role.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRole>> GetRole(int id)
        {
            try
            {
                var query = _rolesRepository.Query(CompanyIds, x => x.Id == id).FirstOrDefault();
                List<UserCompany> userCompanies = new();
                int checkConvertion = 0;
                if(query!=null)
                {
                    query.SystemUserRoles = _systemUserRoleRepository.Query(x => x.SystemRoleId == id)
                        .Include(x => x.SystemUser)
                        .ThenInclude(x=>x.UserCompanies).ToList();
                    userCompanies = query?.SystemUserRoles?.Select(x => x.SystemUser)?.SelectMany(x => x.UserCompanies)?.ToList();
                    foreach (var item in userCompanies)
                    {
                        item.Company = _companyRepository.Query(x=>x.Id == item.CompanyId).FirstOrDefault();
                        if (!int.TryParse(item?.Company?.UsersPerCompany,out checkConvertion))
                        {
                          item.Company.UsersPerCompany = EncryptionHelper.DecryptString(item.Company.UsersPerCompany);
                        }
                    }
                    query.RoleScreens = _roleScreensRepository.Query(x => x.SystemRoleId == id).ToList();
                    query.RoleScreenElements = _roleScreenElementsRepository.Query(x => x.RoleId == id).ToList();
                }

                var role =  query;
                if (role == null) return NotFound();

                var screensQuery = _screensRepository.Query()
                    .Include(x => x.ScreenElements)
                    .OrderBy(x => x.Id);
                var screens = await screensQuery.ToListAsync();
               
                var dto = _mapper.Map<RoleDTO>(role);

                dto.Users = role.SystemUserRoles.Select(x =>
                {
                    var dto = _mapper.Map<UserReadDTO>(x.SystemUser);
                    dto.setAssociatedCompanies();
                    return dto;
                }).ToList();

                dto.Screens = GetRoleScreensDtoList(role, screens);
                dto.companyRoles = _rolesRepository.Query(CompanyIds, x => x.CompanyId == dto.CompanyId)
                                                   .Include(x => x.Company)
                                                    .OrderBy(x => x.Priority).ToList()
                                                    .Select(x =>
                                                    {
                                                        var dto = _mapper.Map<SystemRole>(x);
                                                        dto.ReduceResponseSize();
                                                        return dto;
                                                    }).ToList();
                int check = 0;
                foreach (var item in dto.companyRoles)
                {
                    if (!int.TryParse(item?.Company?.UsersPerCompany, out check))
                        item.Company.UsersPerCompany = EncryptionHelper.DecryptString(item.Company.UsersPerCompany);
                }

                return Ok(dto);
            }
            catch(Exception ex)
            {
                // Log error message
                _logger.LogError("{0}: {1}", ex.Message, ex);

                return BadRequest(new
                {
                    Errors = ex,
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Gets the role screens data.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screens">The screens.</param>
        /// <returns>A list of RoleScreenDTOS.</returns>
        private static List<RoleScreenDTO> GetRoleScreensDtoList(SystemRole role, List<Screen> screens)
        {
            return screens.Select(x =>
            {
                var roleScreen = role.RoleScreens.FirstOrDefault(y => y.ScreenId == x.Id);
                int roleScreenPrivilige = roleScreen != null ? roleScreen.Privilege : RoleScreen.NO_PRIVILEGE;

                return GetRoleScreenDto(role, x, roleScreenPrivilige);
            }).ToList();
        }

        /// <summary>
        /// Gets the role screen dto.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screen">The x.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <returns>A RoleScreenDTO.</returns>
        private static RoleScreenDTO GetRoleScreenDto(SystemRole role, Screen screen, int roleScreenPrivilige)
        {
            return new RoleScreenDTO
            {
                ScreenName = screen.ScreenName,
                ScreenPriviliges = roleScreenPrivilige,
                RoleId = role.Id,
                ScreenId = screen.Id,
                ScreenElementPriviliges = GetRoleScreenElementDtoList(role, screen, roleScreenPrivilige),
            };
        }

        /// <summary>
        /// Gets the screen element priviliges.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="screen">The screen.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <returns>A list of RoleScreenElementDTOS.</returns>
        internal static List<RoleScreenElementDTO> GetRoleScreenElementDtoList(SystemRole role, Screen screen, int roleScreenPrivilige)
        {
            return screen.ScreenElements.Where(y => y.ScreenId == screen.Id).Select(y =>
            {
                int roleScreenElementPrivilige = GetRoleScreenElementPrivilege(role, roleScreenPrivilige, y.Id);
                return new RoleScreenElementDTO
                {
                    ElementName = y.ScreenElementName,
                    Priviliges = roleScreenElementPrivilige,
                    RoleId = role.Id,
                    ScreenElementId = y.Id,
                };
            }).ToList();
        }

        /// <summary>
        /// Gets the role screen element privilege.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="roleScreenPrivilige">The role screen privilige.</param>
        /// <param name="screenElementId">The screen element id.</param>
        /// <returns>An int.</returns>
        private static int GetRoleScreenElementPrivilege(SystemRole role, int roleScreenPrivilige, int screenElementId)
        {
            int roleScreenElementPrivilige;
            if (roleScreenPrivilige == RoleScreen.FULL_CONTROL)
            {
                roleScreenElementPrivilige = RoleScreenElement.FULL_CONTROL;
            }
            else if (roleScreenPrivilige == RoleScreen.NO_PRIVILEGE)
            {
                roleScreenElementPrivilige = RoleScreenElement.NO_PRIVILEGE;
            }
            else // Custom privilege
            {
                var roleScreenElement = role.RoleScreenElements.FirstOrDefault(z => z.ScreenElementId == screenElementId);
                roleScreenElementPrivilige = roleScreenElement != null ?
                                                roleScreenElement.Privilege :
                                                RoleScreenElement.NO_PRIVILEGE;
            }
            return roleScreenElementPrivilige;
        }

        /// <summary>
        /// Puts the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="roleDTO">The role d t o.</param>
        /// <returns>A Task: ActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDTO roleDTO)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || roleDTO == null || id != roleDTO.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            List<int> companyIds = new();
            companyIds.Add(Convert.ToInt32(roleDTO.CompanyId));
            //duplicate role Name validations
            if (roleDTO.IsRoleNameDuplicateCheck)
            {
                roleDTO.Name = roleDTO.Name.Trim(' ','\t');
                if (_rolesRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.Name))
                                                        .Any(d => d.CompanyId == roleDTO.CompanyId &&
                                                                  d.Name == roleDTO.Name))
                    return BadRequest(MsgKeys.RoleDuplicated);
            }

            var role = _mapper.Map<SystemRole>(roleDTO);
            await using var trans = _rolesRepository.GetTransaction();
            try
            {
                _rolesRepository.Update(CompanyIds, role);

                if (roleDTO.Screens != null)
                {
                    foreach (var rscrDto in roleDTO.Screens)
                    {
                        _roleScreensRepository.Delete(x => x.SystemRoleId == role.Id
                                                            && x.ScreenId == rscrDto.ScreenId);
                        _roleScreensRepository.Insert(new RoleScreen
                        {
                            SystemRoleId = role.Id,
                            ScreenId = rscrDto.ScreenId,
                            Privilege = rscrDto.ScreenPriviliges,
                        });

                        if (rscrDto.ScreenElementPriviliges != null)
                        {
                            foreach (var releDto in rscrDto.ScreenElementPriviliges)
                            {
                                _roleScreenElementsRepository.Delete(x => x.RoleId == role.Id
                                                                            && x.ScreenElementId == releDto.ScreenElementId);
                                _roleScreenElementsRepository.Insert(new RoleScreenElement
                                {
                                    RoleId = role.Id,
                                    ScreenElementId = releDto.ScreenElementId,
                                    Privilege = releDto.Priviliges,
                                });
                            }
                        }
                    }
                }

                // Save changes
                _rolesRepository.SaveChanges(UserName,null, trans);
                _roleScreensRepository.SaveChanges(UserName, null, trans);
                _roleScreenElementsRepository.SaveChanges(UserName, null, trans);

                // Commit transaction
                await trans.CommitAsync();
            }
            catch (DbUpdateException e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    Message = MsgKeys.RoleDuplicated
                });
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(_mapper.Map<RoleDTO>(role));
        }

        /// <summary>
        /// Posts the role.
        /// </summary>
        /// <param name="roleDTO">The roleDTO object.</param>
        /// <returns>Action Result of Role</returns>
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleDTO roleDTO)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || roleDTO == null)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }
            List<int> companyIds = new();
            companyIds.Add(Convert.ToInt32(roleDTO.CompanyId));

            roleDTO.Name = roleDTO.Name.Trim(' ','\t');
            //duplicate role Name validations
            if (_rolesRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.Name))
                                                    .Any(d => d.CompanyId == roleDTO.CompanyId &&
                                                              d.Name == roleDTO.Name))
                return BadRequest(MsgKeys.RoleDuplicated);

            var role = _mapper.Map<SystemRole>(roleDTO);
            await using var trans = _rolesRepository.GetTransaction();
            try
            {
                _rolesRepository.Insert(CompanyIds, role);
                _rolesRepository.SaveChanges(UserName, null, trans);

                // Save screen privileges
                SaveScreenPrivileges(roleDTO, role.Id);

                // Save changes
                _roleScreensRepository.SaveChanges(UserName, null, trans);
                _roleScreenElementsRepository.SaveChanges(UserName, null, trans);

                // Commit transaction
                await trans.CommitAsync();
            }
            catch (DbUpdateException e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    Message = MsgKeys.RoleDuplicated
                });
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();

                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);

                // Show Error message
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(_mapper.Map<RoleDTO>(role));
        }

        /// <summary>
        /// Saves the screen privileges.
        /// </summary>
        /// <param name="roleDTO">The role DTO.</param>
        /// <param name="roleId">The role id.</param>
        private void SaveScreenPrivileges(RoleDTO roleDTO, int roleId)
        {
            if (roleDTO.Screens != null)
            {
                foreach (var screen in roleDTO.Screens)
                {
                    _roleScreensRepository.Insert(new RoleScreen
                    {
                        SystemRoleId = roleId,
                        ScreenId = screen.ScreenId,
                        Privilege = screen.ScreenPriviliges,
                    });

                    // Save screen element privileges
                    if (screen.ScreenElementPriviliges != null)
                    {
                        foreach (var element in screen.ScreenElementPriviliges)
                        {
                            _roleScreenElementsRepository.Insert(new RoleScreenElement
                            {
                                RoleId = roleId,
                                ScreenElementId = element.ScreenElementId,
                                Privilege = element.Priviliges,
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Action Result of Role.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var query = _rolesRepository.Query(CompanyIds, x => x.Id == id)
                .Include(x => x.SystemUserRoles);

            var entity = await query.FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            if (entity.SystemUserRoles.Count == 0)
            {
                entity.IsActive = false;
                _rolesRepository.SaveChanges(UserName, null, null);

                return Ok(MsgKeys.DeletedSuccessfully);
            }

            return BadRequest(MsgKeys.RoleChildEntityExists);
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Action Result of Role.</returns>
        [HttpGet("GetRolesByCompanyId/{companyId}")]
        public async Task<IActionResult> GetRolesByCompanyId(int companyId)
        {
            var query = _rolesRepository.Query(CompanyIds, x => x.CompanyId == companyId && x.IsActive == true);
            var result = await query.ToListAsync();

            return Ok(new { Items = result.Select(x => _mapper.Map<RoleDTO>(x)).ToList() });
        }
    }
}
