using System;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class BatchDTO
    {
        public int Id { get; set; }
        [Required]
        public string RequestId { get; set; }
        public int? BusinessUnitId { get; set; }
        
        public int? CustomerId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int BatchStatusId { get; set; }
        public int? LockedBy { get; set; }
        public DateTime? VerifiedStartDate { get; set; }
        public DateTime? VerifiedEndDate { get; set; }
        [Required]
        public int BatchSourceId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? RetriesCount { get; set; }
        [Required]
        public string MandatoryAlerts { get; set; }
        [Required]
        public string ValidationAlerts { get; set; }
        public int? CompanyId { get; set; }
        public int? CurrentOtp { get; set; }
        public DateTime? OtpvalidUntil { get; set; }
        public bool? AppliedGdpr { get; set; }
        public DateTime? RecognizedDate { get; set; }
        public DateTime? StartProcessDate { get; set; }
        public Guid? InternalRequestId { get; set; }
        [Required]
        public string LockedByNavigationId { get; set; }
        public bool? IsActive { get; set; }
        [Required]
        public string LockedByNavigationId1 { get; set; }
    }
}
