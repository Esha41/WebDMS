using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Company
    {
        public Company()
        {
            AdvancedSignatureCallHistories = new HashSet<AdvancedSignatureCallHistory>();
            DocumentRejectionReasonCompanies = new HashSet<DocumentRejectionReasonCompany>();
            DocumentsPerCompanies = new HashSet<DocumentsPerCompany>();
            SystemRoles = new HashSet<SystemRole>();
            UserCompanies = new HashSet<UserCompany>();
        }

        /// <summary>
        /// The fixed company id.
        /// </summary>
        public const int FixedId = 1;
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public int GdprdaysToBeKept { get; set; }
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string ActiveDirectoryDomainName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        [StringLength(50)]
        public string UsersPerCompany { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<AdvancedSignatureCallHistory> AdvancedSignatureCallHistories { get; set; }
        public virtual ICollection<DocumentRejectionReasonCompany> DocumentRejectionReasonCompanies { get; set; }
        public virtual ICollection<DocumentsPerCompany> DocumentsPerCompanies { get; set; }
        public virtual ICollection<CompanyCustomField> CompanyCustomFields { get; set; }
        public virtual ICollection<SystemRole> SystemRoles { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}
