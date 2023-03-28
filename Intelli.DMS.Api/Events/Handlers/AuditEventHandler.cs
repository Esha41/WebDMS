using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Receiver;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Events.Handlers
{
    /// <summary>
    /// The add user event handler.
    /// </summary>
    public class AuditEventHandler : IEventHandler
    {
        private readonly IRepository<Audit> _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventHandler"/> class.
        /// </summary>
        /// <param name="auditRepository">The Audit user repository.</param>
        public AuditEventHandler(IRepository<Audit> auditRepository)
        {
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="eventMsg">The event msg.</param>
        public void HandlerEvent(string eventMsg)
        {
            var eventObj = JsonConvert.DeserializeObject<MQEvent<List<AuditEntry>>>(eventMsg);

            var auditEntries = eventObj.Model;

            foreach (AuditEntry auditEntry in auditEntries)
            {
                if(auditEntry.AuditUser != null)
                 _auditRepository.Insert(auditEntry.ToAudit());
            }

            _auditRepository.SaveChanges(nameof(AuditEventHandler),null,null);
        }
    }
}
