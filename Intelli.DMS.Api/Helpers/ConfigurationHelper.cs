using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Controllers;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Helpers
{
    /// <summary>
    /// The configuration helper class.
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// The default configuration record id.
        /// </summary>
        const int DEFAULT_ID = 1;

        static ConfigurationDto _dto = null;

        /// <summary>
        /// Reads the configurations from database.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The auto mapper.</param>
        /// <returns>A ConfigurationDto.</returns>
        public static ConfigurationDto Read(DMSContext context, IMapper mapper)
        {
            if (_dto == null)
            {
                var entity = new GenericRepository<Configuration>(context).GetById(DEFAULT_ID).Result;
                _dto = mapper.Map<ConfigurationDto>(entity);
            }
            return _dto;
        }

        /// <summary>
        /// Updates the configurations in database.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The auto mapper.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="dto">The ConfigurationDto.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>A ConfigurationDto.</returns>
        public static ConfigurationDto Update(DMSContext context, IMapper mapper, IEventSender sender, ConfigurationDto dto, string userName)
        {
            var repository = new GenericRepository<Configuration>(context)
            {
                AfterSave = (logs) =>
                     sender.SendEvent(new MQEvent<List<AuditEntry>>(BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs))
            };

            var entity = mapper.Map<Configuration>(dto);
            entity.Id = DEFAULT_ID;
            entity.IsActive = true;
            repository.SaveChanges(userName,null, null);

            return _dto = mapper.Map<ConfigurationDto>(entity);
        }
    }
}
