using Intelli.DMS.Api.Events.Handlers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Receiver;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Events
{
    /// <summary>
    /// The queue handler mapping factory.
    /// </summary>
    public class QueueHandlerMappingFactory : IQueueHandlerMappingFactory
    {
        private readonly IRepository<Audit> _auditRepository;
        private IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueHandlerMappingFactory"/> class.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        public QueueHandlerMappingFactory(IServiceProvider provider,IConfiguration configuration)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetService<DMSAuditContext>();
            _auditRepository = new GenericRepository<Audit>(context);
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the queue to handler mapping.
        /// </summary>
        /// <returns>A Dictionary of event handlers.</returns>
        public Dictionary<string, IEventHandler> GetQueueToHandlerMapping()
        {
            BaseController.AUDIT_EVENT_NAME = _configuration.GetValue<string>("RabbitMq_Event_Name");

            Dictionary<string, IEventHandler> result = new()
            {
                [BaseController.AUDIT_EVENT_NAME] = new AuditEventHandler(_auditRepository)
            };
            return result;
        }
    }
}
