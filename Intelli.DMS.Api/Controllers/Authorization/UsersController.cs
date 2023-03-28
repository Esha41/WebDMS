﻿using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
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
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncryptionHelper = Intelli.DMS.Api.Helpers.EncryptionHelper;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The users controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<SystemUser> _userRepository;
        private readonly IRepository<SystemUserRole> _userRoleRepository;
        private readonly IRepository<UserPreference> _userPreferenceRepository;
        private readonly IRepository<UserCompany> _userCompanyRepository;
        private readonly IRepository<SystemUserView> _userViewRepository;
        private readonly IRepository<SystemUserRole> _systemUserRoleRepository;
        private readonly IRepository<Company> _companyRepository;

        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        /// <param name="mapper">The DTO mapper.</param>
        /// <param name="logger">The ILogger logger.</param>
        /// <param name="sender">The IEventSender event publisher.</param>
        /// <param name="authService"></param>
        public UsersController(DMSContext context,
            IMapper mapper,
            ILogger<UsersController> logger,
            IEventSender sender,
            IAuthService authService)
        {
            _userRepository = new GenericRepository<SystemUser>(context);
            _userRoleRepository = new GenericRepository<SystemUserRole>(context);
            _userPreferenceRepository = new GenericRepository<UserPreference>(context);
            _userCompanyRepository = new GenericRepository<UserCompany>(context);
            _userViewRepository = new GenericRepository<SystemUserView>(context);
            _systemUserRoleRepository = new GenericRepository<SystemUserRole>(context);
            _companyRepository = new GenericRepository<Company>(context);

            ((GenericRepository<SystemUserRole>)_systemUserRoleRepository).AfterSave =
            ((GenericRepository<UserCompany>)_userCompanyRepository).AfterSave =
            ((GenericRepository<SystemUser>)_userRepository).AfterSave =
            ((GenericRepository<UserPreference>)_userPreferenceRepository).AfterSave =
            ((GenericRepository<SystemUserRole>)_userRoleRepository).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>ActionResult of Response</returns>
        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get User Called with params: {0}", queryStringParams);
            PagedResult<UserReadDTO> result = null;
            try
            {
                queryStringParams.SetFilterExpression(
                    queryStringParams?.FilterExpression?.Replace("Company.CompanyName", "CompanyName"));

                queryStringParams.SetOrderBy(queryStringParams.OrderBy.Replace("Company", "CompanyName"));

                var queryResult = _userViewRepository.Get(
                                        queryStringParams.FilterExpression,
                                        queryStringParams.OrderBy,
                                        queryStringParams.PageSize,
                                        queryStringParams.PageNumber);

                queryResult.List = queryResult.List.ToList();


                var usersList = queryResult.List.Select(x => x.Id).ToList();
                var FilterResult = await FilterUserAgainstCompaniesId(usersList);
                int total = queryResult.Count;

                //if user not super admin
                if (CompanyIds.Count > 0)
                   total = FilterResult.Count;

                var userList = FilterResult.List;
                queryResult.Count = FilterResult.Count;

                result = new PagedResult<UserReadDTO>(
                                 total,
                                 queryStringParams.PageNumber,
                                 userList,
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
        /// Get all active users.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetActive()
        {
            var dto = new UserReadDTO();

            var user = _userCompanyRepository.Query().Where(x => CompanyIds.Contains(x.CompanyId)).Select(x => x.SystemUserId).ToList();
            var result = await _userRepository.Query(x => x.IsActive == true)
                                              .Where(x => user.Contains(x.Id))
                                              .OrderBy(x => x.FullName)
                                              .Include(x => x.UserCompanies)
                                              .ThenInclude(x => x.Company)
                                              .Select(x => new { User = x, UserCompaniesList = x.UserCompanies.Select(y => y.Company).ToList() })
                                              .ToListAsync();
            int check = 0;
            return Ok(new
            {
                Items = result.Select(x =>
            {
                var dto = _mapper.Map<UserReadDTO>(x.User);
                dto.UserCompanies = null;
                x.UserCompaniesList.ForEach(y => y.UsersPerCompany = (!int.TryParse(y.UsersPerCompany, out check))
                                                                      ? EncryptionHelper.DecryptString(y.UsersPerCompany)
                                                                      : y.UsersPerCompany);
                dto.Companies = x.UserCompaniesList.Select(y => _mapper.Map<CompanyDTO>(y)).ToList();
                return dto;
            }).ToList()
            });
        }

        /// <summary>
        /// Gets the user with user id.
        /// </summary>
        /// <param name="id">The id of the User.</param>
        /// <returns>ActionResult of Response</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var q = _userRepository.Query(r => r.Id == id)
                                    .Include(x => x.SystemUserRoles)
                                    .ThenInclude(x => x.SystemRole)
                                    .Include(x => x.SystemUserCountries)
                                    .ThenInclude(x => x.Country)
                                    .Include(x => x.UserCompanies);

            var user = await q.FirstOrDefaultAsync();
            if (user == null) return NotFound();
            foreach (var item in user.UserCompanies)
            {
                    item.Company = _companyRepository.Query(x => x.Id == item.CompanyId).FirstOrDefault();
                    item.Company.UsersPerCompany = EncryptionHelper.DecryptString(item.Company.UsersPerCompany);
            }
            var dto = _mapper.Map<UserReadDTO>(user);

            dto.Roles = user.SystemUserRoles.Select(x => _mapper.Map<RoleDTO>(x.SystemRole));
            dto.Countries = user.SystemUserCountries.Select(x => _mapper.Map<CountryDTO>(x.Country));

            dto.RoleIds = dto.Roles?.Select(x => x.Id).ToList();
            dto.CountryIds = dto.Countries?.Select(x => x.Id).ToList();
            dto.CompanyIds = dto.UserCompanies?.Select(x => x.Company.Id).ToList();
            dto.UserCompanies = new List<UserCompany>();
            return Ok(dto);
        }

        /// <summary>
        /// Gets the specific user roles.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Array of the user's roles.</returns>
        [HttpGet]
        [Route("Roles/{userId}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var q = _userRoleRepository
                .Query(r => r.SystemUserId == userId)
                .Include(x => x.SystemRole);
            var list = await q.ToListAsync();

            return Ok(new { items = list.Select(x => _mapper.Map<RoleDTO>(x.SystemRole)) });
        }

        /// <summary>
        /// Add new user including insertion of user in Microsoft Identity framework.
        /// </summary>
        /// <param name="dto">The dto containing UserName, Email and Password.</param>
        /// <returns>Async ActionResult (JSON)</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserReadDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(new { Message = MsgKeys.InvalidInputParameters });
            }
            if (dto.CompanyIds == null)
            {
                return BadRequest(new { Message = MsgKeys.CompanyMustBeSelected });
            }
            if (dto.CompanyIds.Count == 0)
            {
                return BadRequest(new { Message = MsgKeys.CompanyMustBeSelected });
            }
            var allUsersOfCompany = _userCompanyRepository.Query(x => dto.CompanyIds.Contains(x.CompanyId))
                                                            .Select(x => x.SystemUserId)
                                                             .ToList();
            
            dto.Email = dto.Email.Trim(' ','\t');
            //duplicate Outlook Email field validations
            if (_userRepository.Query(x=>allUsersOfCompany.Contains(x.Id))
                                                        .Any(d => d.OutlookEmail == dto.Email))
                return BadRequest(MsgKeys.UserDuplicated);

            // Registering the user
            var result = await _authService.RegisterUser(dto);

            if (result.Error != null)
            {
                return BadRequest(new { Message = (string.IsNullOrWhiteSpace(result.Message) ? MsgKeys.UserRegistrationFailed : result.Message), Errors = GetErrorMessageDictionary(result.Error.Message) });
            }

            // If registration failed
            if (!result.IdentityResult.Succeeded)
            {
                var dictionary = new Dictionary<string, string>();

                // Collect all errors in a dictionary
                foreach (var error in result.IdentityResult.Errors)
                {
                    dictionary[error.Code] = error.Description;
                }

                // Passing the errors dictionary to the json response
                return BadRequest(new { Message = (string.IsNullOrWhiteSpace(result.Message) ? MsgKeys.UserRegistrationFailed : result.Message), Errors = dictionary });
            }

            // Registration passed
            return Ok(_mapper.Map<UserReadDTO>(result.User));
        }

        /// <summary>
        /// Gets the error message dictionary.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>A Dictionary object.</returns>
        private static Dictionary<string, string> GetErrorMessageDictionary(string error)
        {
            return new Dictionary<string, string>
            {
                [error] = error
            };
        }

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="id">The id of the item to be updated</param>
        /// <param name="userDTO">The DTO containing the values to be updated.</param>
        /// <returns>ActionResult of Response object</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Put(int id, UserReadDTO userDTO)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid ||
                userDTO == null ||
                id != userDTO.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }
            if (userDTO.CompanyIds == null)
            {
                return BadRequest(new { Message = MsgKeys.CompanyMustBeSelected });
            }
            if (userDTO.CompanyIds.Count == 0)
            {
                return BadRequest(new { Message = MsgKeys.CompanyMustBeSelected });
            }
            if (userDTO.IsOutlookEmailChanged)
            {
                var allUsersOfCompany = _userCompanyRepository.Query(x => userDTO.CompanyIds.Contains(x.CompanyId))
                                                            .Select(x => x.SystemUserId)
                                                             .ToList();

                userDTO.Email = userDTO.Email.Trim(' ','\t');
                //duplicate Outlook Email field validations
                if (_userRepository.Query(x => allUsersOfCompany.Contains(x.Id))
                                                            .Any(d => d.OutlookEmail == userDTO.OutlookEmail || 
                                                                      d.Email == userDTO.OutlookEmail))
                    return BadRequest(MsgKeys.RecordWithSameOutLookEmailExists);
            }

            var result = await _authService.UpdateUser(userDTO);
            if (result.Error != null)
            {
                return BadRequest(new { Message = (string.IsNullOrWhiteSpace(result.Message) ? MsgKeys.UserUpdationFailed : result.Message), Errors = GetErrorMessageDictionary(result.Error.Message) });
            }

            // If update failed
            if (!result.IdentityResult.Succeeded)
            {
                var dictionary = new Dictionary<string, string>();

                // Collect all errors in a dictionary
                foreach (var error in result.IdentityResult.Errors)
                {
                    dictionary[error.Code] = error.Description;
                }

                // Passing the errors dictionary to the json response
                return BadRequest(new { Message = (string.IsNullOrWhiteSpace(result.Message) ? MsgKeys.UserUpdationFailed : result.Message), Errors = dictionary });
            }

            _logger.LogInformation("User updated with id: {0}", id);

            // Update passed
            return Ok(_mapper.Map<UserReadDTO>(result.User));
        }

        /// <summary>
        /// Deletes the user by providing its id.
        /// </summary>
        /// <param name="id">The id of the User</param>
        /// <returns>ActionResult of Response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (SystemUser.IsDefault(id)) return BadRequest(MsgKeys.NotAllowed);

            var query = _userRepository.Query(x => x.Id == id);

            var entity = await query.FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            entity.IsActive = false;
            _userRepository.SaveChanges(UserName, null, null);

            return Ok(MsgKeys.DeletedSuccessfully);
        }

        /// <summary>
        /// Changes the username and password.
        /// </summary>
        /// <param name="id">Identity of the user.</param>
        /// <param name="dto">The username and password dto.</param>
        /// <returns>A Task of action result.</returns>
        [HttpPut]
        [Route("UserCredentials/{id}")]
        public async Task<IActionResult> PutUserCredentials(int id, UserCredentialsDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
            {
                return BadRequest(MsgKeys.InvalidInputParameters);
            }

            var result = await _authService.UpdateUserCredentials(dto);
            if (result.Error != null)
            {
                return BadRequest(new { Message = MsgKeys.UserUpdationFailed, Errors = GetErrorMessageDictionary(result.Error.Message) });
            }

            // If update failed
            if (!result.IdentityResult.Succeeded)
            {
                var dictionary = new Dictionary<string, string>();

                // Collect all errors in a dictionary
                foreach (var error in result.IdentityResult.Errors)
                {
                    dictionary[error.Code] = error.Description;
                }

                // Passing the errors dictionary to the json response
                return BadRequest(new { Message = MsgKeys.UserUpdationFailed, Errors = dictionary });
            }

            // Update passed
            return Ok(MsgKeys.UpdatedSuccessfully);
        }

        /// <summary>
        /// Resends the email confirmation.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A Task of action result.</returns>
        [HttpGet]
        [Route("ResendEmailConfirmation/{id}")]
        public async Task<IActionResult> ResendEmailConfirmation(int id)
        {
            if (await _authService.ResendEmailConfirmation(id))
            {
                _logger.LogInformation("Email confirmation link sent to user with id: {0}", id);

                return Ok(MsgKeys.EmailConfirmationLinkSuccess);
            }
            return BadRequest(MsgKeys.EmailConfirmationLinkFailure);
        }

        /// <summary>
        /// Insert new user preferences.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dto">The UserPreferencesDTO.</param>
        /// <returns>A Success or response message.</returns>
        [HttpPut("Preferences/{userId}")]
        public async Task<ActionResult> PutPreferences(int userId, UserPreferencesDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
                return BadRequest(MsgKeys.InvalidInputParameters);

            var query = _userPreferenceRepository.Query(x => x.SystemUserId == userId).AsNoTracking();
            var pref = await query.FirstOrDefaultAsync();

            var model = _mapper.Map<UserPreference>(dto);
            model.SystemUserId = userId;

            if (pref != null)
            {
                model.Id = pref.Id;
                model.IsActive = pref.IsActive;
                model.CreatedAt = pref.CreatedAt;
                model.UpdatedAt = pref.UpdatedAt;

                _userPreferenceRepository.Update(model);
            }
            else
                _userPreferenceRepository.Insert(model);

            // Save changes
            _userPreferenceRepository.SaveChanges(UserName, null, null);

            return Ok(MsgKeys.UpdatedSuccessfully);
        }

        /// <summary>
        /// Insert new user preferences.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dto">The UserPreferencesDTO.</param>
        /// <returns>A Success or response message.</returns>
        [HttpGet("Preferences/{userId}")]
        public async Task<ActionResult> GetPreferences(int userId)
        {
            var query = _userPreferenceRepository.Query(x => x.SystemUserId == userId);
            var pref = await query.FirstOrDefaultAsync();

            var dto = _mapper.Map<UserPreferencesDTO>(pref);
            if (pref == null) dto = new UserPreferencesDTO();

            return Ok(dto);
        }

        private async Task<QueryResult<UserReadDTO>> FilterUserAgainstCompaniesId(List<int> userList)
        {
            QueryResult<UserReadDTO> newResult = new();
            newResult.List = new List<UserReadDTO>();
            newResult.Count = newResult.List.Count;

            // if user is not a super admin
            if (CompanyIds.Count > 0)
            {


                var user = _userCompanyRepository.Query(x => userList.Contains(x.SystemUserId))
                    .Where(x => CompanyIds.Contains(x.CompanyId))
                    .Select(x => x.SystemUserId).ToList();

                var result = await _userRepository.Query()
                                                  .Where(x => user.Contains(x.Id))
                                                  .OrderBy(x => x.FullName)
                                                  .Include(x => x.UserCompanies)
                                                  .ThenInclude(x => x.Company)
                                                  .Include(x => x.SystemUserRoles)
                                                  .ThenInclude(x => x.SystemRole)
                                                  .Select(x => new
                                                  {
                                                      User = x,
                                                      UserCompaniesList = x.UserCompanies.Select(y => y.Company).ToList()
                                                  ,
                                                      UserRolesList = x.SystemUserRoles.Select(y => y.SystemRole)
                                                  })
                                                  .ToListAsync();
                int checkUserPerCompany = 0;

                var data = result.Select(x =>
                {
                    var dto = _mapper.Map<UserReadDTO>(x.User);
                    dto.UserCompanies = null;
                    foreach (var item in x.UserCompaniesList)
                    {
                        if(!int.TryParse(item.UsersPerCompany,out checkUserPerCompany))
                        {
                            item.UsersPerCompany = EncryptionHelper.DecryptString(item.UsersPerCompany);
                        }
                    }
                    dto.Companies = x.UserCompaniesList.Select(y => _mapper.Map<CompanyDTO>(y)).ToList();
                    string companyNames = "";
                    foreach (var item in dto.Companies?.Select(x => x.CompanyName)?.ToList())
                    {
                        companyNames += $"{item},";
                    }
                    if (companyNames.Length > 1)
                    {
                        dto.Company = companyNames.Remove(companyNames.Length - 1, 1).ToString();
                    }
                    dto.Roles = x.UserRolesList.Select(y => _mapper.Map<RoleDTO>(y)).ToList();
                    return dto;
                }).ToList();


                newResult.List = data;

                newResult.Count = newResult.List.Count;
                return FilterUserBasedUponRoles(newResult);
            }

            var QueryResult = await _userRepository.Query()
                                                  .Where(x => userList.Contains(x.Id))
                                                  .OrderBy(x => x.FullName)
                                                  .Include(x => x.UserCompanies)
                                                  .ThenInclude(x => x.Company)
                                                  .Select(x => new { User = x, UserCompaniesList = x.UserCompanies.Select(y => y.Company).ToList() })
                                                  .ToListAsync();

            List<Company> companies = new();
            int check = 0;
            var QueryData = QueryResult.Select(x =>
            {
                var dto = _mapper.Map<UserReadDTO>(x.User);
                dto.UserCompanies = null;
                companies = x.UserCompaniesList;
                foreach (var item in companies)
                {
                    if(!int.TryParse(item.UsersPerCompany, out check))
                    {
                      item.UsersPerCompany = EncryptionHelper.DecryptString(item.UsersPerCompany);
                    }
                }
                dto.Companies = x.UserCompaniesList.Select(y => _mapper.Map<CompanyDTO>(y)).ToList();
                string companyNames = "";
                foreach (var item in dto.Companies?.Select(x=>x.CompanyName)?.ToList())
                {
                    companyNames += $"{item},";
                }
                if (companyNames.Length > 1)
                {
                    dto.Company = companyNames.Remove(companyNames.Length - 1, 1).ToString();
                }
                return dto;
            }).ToList();

            newResult.List = QueryData;
            newResult.Count = newResult.List.Count;
            return newResult;

        }

        private QueryResult<UserReadDTO> FilterUserBasedUponRoles(QueryResult<UserReadDTO> queryResult)
        {

            QueryResult<UserReadDTO> newResult = new();
            newResult.List = new List<UserReadDTO>();
            var userRoles = _userRepository.Query(x => x.Email == UserName).Select(x => x.Id);
            var systemUserRoles = _systemUserRoleRepository.Query(x => x.SystemUserId == userRoles.FirstOrDefault())
                                                           .Include(x => x.SystemRole)
                                                           .Include(x => x.SystemUser)
                                                           .ThenInclude(x=>x.UserCompanies)
                                                           .OrderBy(x => x.SystemRole.Priority)
                                                           .ToList().FirstOrDefault();

            foreach (var user in queryResult.List)
            {
                var filteredData = user.Roles.ToList().OrderBy(x => x.Priority).FirstOrDefault();
                if ((filteredData != null && 
                    filteredData.Priority >= systemUserRoles.SystemRole.Priority)
                    || (!user.Roles.Any() && systemUserRoles.SystemRole.Priority == 1 ))
                {
                    newResult.List.Add(user);
                }
            }

            newResult.Count = newResult.List.Count;
            return newResult;
        }
    }
}
