using Intelli.DMS.Api.DTO;
using Intelli.DMS.Auth.Api.Models;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.ActiveDirectory.Impl
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ActiveDirectoryService> _logger;
        private readonly DMSContext _context;
        private readonly IRepository<SystemUser> _userRepository;
        private readonly IRepository<UserCompany> _userCompanyRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly string _userName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="config">The config object to be Injected via IOC</param>
        /// <param name="context">The authorization database context.</param>
        /// <param name="sender">The Event Sender.</param>
        /// <param name="httpContext">The current Http context.</param>
        /// <param name="logger">Logs information and errors.</param>

        public ActiveDirectoryService(IConfiguration config,
            DMSContext context,
            IEventSender sender,
            IHttpContextAccessor httpContext,
             ILogger<ActiveDirectoryService> logger)
        {
            if (httpContext != null)
            {
                _userName = httpContext.HttpContext.Request.Headers["UserName"];
                _userName = string.IsNullOrEmpty(_userName) ? "System" : _userName;
            }

            _config = config;
            _logger = logger;
            _context = context;

            _userRepository = new GenericRepository<SystemUser>(context);
            _userCompanyRepository = new GenericRepository<UserCompany>(context);
            _companyRepository = new GenericRepository<Company>(context);

            ((GenericRepository<UserCompany>)_userCompanyRepository).AfterSave =
           ((GenericRepository<SystemUser>)_userRepository).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

        }

        /// <summary>
        /// Validates the active directory user.
        /// </summary>
        /// <param name="loginDTO">The login dto.</param>
        /// <returns>An UserLoginResult.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Will be used on windows machine")]
        public async Task<UserLoginResult> ValidateActiveDirectoryUser(LoginDTO loginDTO)
        {
            var loginResult = new UserLoginResult() { IdentityResult = IdentityResult.Failed() };

            var contextType = GetContextType();
            var domainName = GetDomainName();

            using var adContext = new PrincipalContext(contextType, domainName);
            var result = adContext.ValidateCredentials(GetADUserName(loginDTO.Email), loginDTO.Password);
            if (result)
            {
                var userPrincipal = UserPrincipal.FindByIdentity(adContext, loginDTO.Email);
                if (userPrincipal.Enabled != null && !userPrincipal.Enabled.Value)
                {
                    loginResult.Message = MsgKeys.UserDeactivated;
                }
                else if (userPrincipal.IsAccountLockedOut())
                {
                    loginResult.Message = MsgKeys.UserLoginFailed;
                }
                else
                {
                    loginResult.IdentityResult = IdentityResult.Success;
                    loginResult.User = await GetActiveDirectoryUserFromDatabase(loginDTO.Email);
                }
            }
            else
            {
                loginResult.Message = MsgKeys.PasswordIsIncorrect;
            }
            return loginResult;
        }

        /// <summary>
        /// Are the user is in active directory.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>A bool.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Will be used on windows machine")]
        public bool IsUserInActiveDirectory(string email)
        {
            if (!GetADIntegrationEnabled()) return false;

            var contextType = GetContextType();
            var domainName = GetDomainName();

            using var adContext = new PrincipalContext(contextType, domainName);

            var user = UserPrincipal.FindByIdentity(adContext, GetADUserName(email));

            return user != null;
        }

        /// <summary>
        /// Gets the active directory user from database.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>A Domain.Core.Model.User.</returns>
        private async Task<SystemUser> GetActiveDirectoryUserFromDatabase(string emailAddress)
        {
            var query = _userRepository.Query(x => x.Email.ToLower() == emailAddress.ToLower());
            var result = _userRepository.Get(query);

            if (result.Count > 0) return result.List[0];

            // If user not found, create it.
            var user = new SystemUser
            {
                Email = emailAddress,
                IsActiveDirectoryUser = true,
                //outlook email not required and used in case of Active Directory User. Added to prevent Error.
                OutlookEmail = emailAddress
            };

            await using (var transaction = _userRepository.GetTransaction())
            {
                try
                {
                    string CompanyActiveDirectoryDomainName = user.Email.Split('@').Last();
                    var company = _companyRepository.Query(x => x.ActiveDirectoryDomainName == CompanyActiveDirectoryDomainName)
                                                     .FirstOrDefault();
                    if (company != null)
                    {
                        _userRepository.Insert(user);
                        _userRepository.SaveChanges(_userName, null, transaction);

                        UserCompany userCompany = new() { CompanyId = company.Id, SystemUserId = user.Id };
                        _userCompanyRepository.Insert(userCompany);
                        _userCompanyRepository.SaveChanges(_userName, null, transaction);

                        // Set Default Preferences
                        new PreferencesHelper(_context).SetDefaultAsync(user.Id, _userName).Wait();

                        // Commit transaction
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        throw new Exception(MsgKeys.CompanyNotExsitsWith_AD_DomainName);
                    }
                }
                catch (Exception e)
                {
                    // Rollback transaction
                    await transaction.RollbackAsync();

                    // Log error message
                    _logger.LogError("{0}: {1}", e.Message, e);
                }
            }
            return user;
        }

        /// <summary>
        /// Gets the active directory integration enabled or not.
        /// </summary>
        /// <returns>A bool.</returns>
        private bool GetADIntegrationEnabled()
        {
            var enabled = _config["ADIntegration"];

            if (string.IsNullOrWhiteSpace(enabled))
                return false;

            return string.Compare(enabled, "On", true) == 0;
        }

        /// <summary>
        /// Gets the domain name.
        /// </summary>
        /// <returns>A string.</returns>
        private string GetDomainName()
        {
            var domainName = _config["DomainName"];

            if (string.IsNullOrWhiteSpace(domainName))
                return Environment.UserDomainName;

            return domainName;
        }

        /// <summary>
        /// Gets the directory services context type.
        /// </summary>
        /// <returns>A ContextType.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Will be used on windows machine")]
        private ContextType GetContextType()
        {
            var contextType = _config["DirectoryContextType"];

            if (string.IsNullOrWhiteSpace(contextType))
                return ContextType.Machine;

            return string.Compare(contextType, "Machine", true) == 0 ? ContextType.Machine : ContextType.Domain;
        }

        /// <summary>
        /// Gets the active directory user name.
        /// </summary>
        /// <param name="email">The email address of user.</param>
        /// <returns>A user name.</returns>
        static string GetADUserName(string email)
        {
            return email.Split('@')[0];
        }
    }
}
