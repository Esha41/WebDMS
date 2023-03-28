using Intelli.DMS.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.Views
{
    public class ClientsDataToBeDeleted : IEntity
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? BusinessUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BatchStatusId { get; set; }
        public int? LockedBy { get; set; }
        public DateTime? VerifiedStartDate { get; set; }
        public DateTime? VerifiedEndDate { get; set; }
        public int BatchSourceId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? RetriesCount { get; set; }
        public string MandatoryAlerts { get; set; }
        public string ValidationAlerts { get; set; }
        public int? CurrentOtp { get; set; }
        public DateTime? OtpvalidUntil { get; set; }
        public bool? AppliedGdpr { get; set; }
        public DateTime? RecognizedDate { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public Guid? InternalRequestId { get; set; }
        public string LockedByNavigationId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int? CustomerId { get; set; }
        public int? CompanyId { get; set; }

        public int? Cid { get; set; }
    }
}
