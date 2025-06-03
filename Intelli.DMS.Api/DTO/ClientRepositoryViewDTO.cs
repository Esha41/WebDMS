using Intelli.DMS.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class ClientRepositoryViewDTO
    {
        public int Id { get; set; }
        public bool AppliedGDPR { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RepositoryName { get; set; }
        public bool? IsActive { get; set; }
    }
}
