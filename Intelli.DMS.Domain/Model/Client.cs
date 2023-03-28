using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Client
    {
        public Client()
        {
            Batches = new HashSet<Batch>();
            ClientTags = new HashSet<ClientTag>();
            ClientCompanyCustomFieldValues = new HashSet<ClientCompanyCustomFieldValue>();
        }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MaxLength(20)]
        [StringLength(20)]
        public string JMBG { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public bool IsNotValidForTransaction { get; set; }
        public string Reason { get; set; }
        public int GdprdaysToBeKept { get; set; }
        
        [StringLength(50)]
        public string ExternalId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<ClientTag> ClientTags { get; set; }
        public virtual ICollection<ClientCompanyCustomFieldValue> ClientCompanyCustomFieldValues { get; set; }

    }
}
