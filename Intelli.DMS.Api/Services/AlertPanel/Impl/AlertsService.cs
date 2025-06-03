
using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.Base;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The Alerts service implementation
    /// </summary>
    public class AlertsService : BaseService, IAlertsService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Alert> _repositoryAlert;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<SystemUser> _repositorySystemUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// 
        public AlertsService(DMSContext context,
            IMapper mapper, IHttpContextAccessor accessor,
            ILogger<AlertsService> logger,IEventSender sender)
            : base(accessor)
        {
            _repositoryAlert = new GenericRepository<Alert>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);
            _repositorySystemUser = new GenericRepository<SystemUser>(context);
            _mapper = mapper;
            _logger = logger;

            ((GenericRepository<Alert>)_repositoryAlert).AfterSave =
            ((GenericRepository<SystemUserRole>)_repositorySystemUserRole).AfterSave =
            ((GenericRepository<SystemUser>)_repositorySystemUser).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(Shared.Mvc.Controllers.BaseController.AUDIT_EVENT_NAME,
                    (List<AuditEntry>)logs));
            };
        }

        /// <summary>
        /// Marks alert as read.
        /// </summary>
        /// <param name="id">The Alert id.</param>
        /// <returns>A Task of Alert DTO.</returns>
        public async Task<AlertDTO> UpdateAlert(int id)
        {
            var entity = await _repositoryAlert.GetById(id);

            entity.IsRead = true;

            _repositoryAlert.Update(entity);
            _repositoryAlert.SaveChanges(UserName,null,null);
            return _mapper.Map<AlertDTO>(entity);
        }

        /// <summary>
        /// Marks All alert as read of User Id.
        /// </summary>
        /// <param name="userId">The User id.</param>
        /// <returns> </returns>
        public void UpdateAlertReadAllAlerts(int userId)
        {
            var entityList =  _repositoryAlert.Query(x=>x.SystemUserId == userId).ToList();
            entityList.ForEach(x=>x.IsRead = true);
            _repositoryAlert.UpdateRange(entityList);
            _repositoryAlert.SaveChanges(UserName,null,null);
        }

        /// <summary>
        /// Get all alerts by system user id.
        /// </summary>
        /// <param name="systemUserId">The System User id.</param>
        /// <returns>A Task List of Alert DTO.</returns>

        public List<AlertDTO> GetAllAlertsBySystemUserId(int systemUserId)
        {
            var query = _repositoryAlert.Query(x => x.SystemUserId == systemUserId && x.IsRead == false);

            var result =  query.ToListAsync().Result.OrderByDescending(x=>x.Id).ToList();
            return _mapper.Map<List<AlertDTO>>(result);
        }

        /// <summary>
        /// Add Alert .
        /// </summary>
        /// <param name="dto">The Alert DTO.</param>
        /// <returns> A  Alert DTO.</returns>

        public AlertDTO AddAlert(AlertDTO dto)
        {
            var entity = _mapper.Map<Alert>(dto);

            _repositoryAlert.Insert(entity, true);
            _repositoryAlert.SaveChanges(UserName,null, null);

            return _mapper.Map<AlertDTO>(entity);
        }

        /// <summary>
        ///  Add alert to specific role
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns>A  bool.</returns>
        public async Task<bool> SendAlertsToRole(string msg, int roleId)
        {
            // Add alert to specific role
            await using var trans = _repositoryAlert.GetTransaction();
            try
            {
                var query = _repositorySystemUserRole.Query(x => x.SystemRoleId == roleId);

                var userListAgaistRole = await query.ToListAsync();

                AlertDTO dto = new AlertDTO();

                foreach (var user in userListAgaistRole)
                {
                    var entity = _mapper.Map<Alert>(dto);

                    entity.SystemUserId = user.SystemUserId;
                    entity.Msg = msg;

                    _repositoryAlert.Insert(entity, true);
                }

                _repositoryAlert.SaveChanges(UserName,null,null);

                await trans.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
            
        }

        /// <summary>
        ///  Add alert to specific user
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <param name="systemUserId">The system user id.</param>
        /// <returns>A  bool.</returns>
        public bool SendAlertToUser(string msg, int systemUserId)
        {
            // Add alert to specific user
            AlertDTO dto = new AlertDTO();

            var entity = _mapper.Map<Alert>(dto);
            entity.SystemUserId = systemUserId;

            _repositoryAlert.Insert(entity, true);
            _repositoryAlert.SaveChanges(UserName,null,null);

            return true;
        }

        /// <summary>
        ///  Add alert msg to all users in the system which are active
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <returns>A  bool.</returns>
        public async Task<bool> BroadcastAlert(string msg)
        {
            // Add alert msg to all users in the system which are active

            await using var trans = _repositoryAlert.GetTransaction();
            try
            {
                var query = _repositorySystemUser.Query(x => x.IsActive == true);

                var activeUsersList = await query.ToListAsync();

                AlertDTO dto = new AlertDTO();

                foreach (var user in activeUsersList)
                {
                    var entity = _mapper.Map<Alert>(dto);

                    entity.SystemUserId = user.Id;
                    entity.Msg = msg;

                    _repositoryAlert.Insert(entity, true);
                }

                _repositoryAlert.SaveChanges(UserName,null,null);

                await trans.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);
                throw;
            }
        }
    }
}
