using Intelli.DMS.Domain.Model;
using System;

namespace Intelli.DMS.Api.DTO
{
    public class ClientTagDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? CreatedById { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public string RecordStatus { get; set; }

        public string CreatedByFullName = $"{ CreatedBy?.FullName }";
        public static SystemUser CreatedBy { get; set; }

    }
}
