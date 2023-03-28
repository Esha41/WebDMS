using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The company DTO.
    /// </summary>
    public class CompanyDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// 
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CompanyCode { get; set; }


        /// <summary>
        /// Gets or sets the GDPR days to be kept.
        /// </summary>
        /// 
        public int GdprdaysToBeKept { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// 
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// 
        [Required]
        [MaxLength(150)]
        public string ActiveDirectoryDomainName { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is Company Code Change.
        /// </summary>
        public bool IsCompanyCodeChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is UserName Change.
        /// </summary>
        public bool IsUserNameChange { get; set; }



        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// 
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// 
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the documents per companies.
        /// </summary>
        public virtual IEnumerable<DocumentsPerCompanyDTO> DocumentsPerCompanies { get; set; }

        /// <summary>
        /// Gets or sets the system users.
        /// </summary>
        public virtual IEnumerable<UserReadDTO> SystemUsers { get; set; }

        /// <summary>
        /// Gets or sets the users per company.
        /// </summary>
        /// 
        [Required]
        public int UsersPerCompany { get; set; }

        /// <summary>
        /// Gets or sets the List Company Custom Fields.
        /// </summary>
        public List<CompanyCustomFieldDTO> Fields { get; set; }
        /// <summary>
        /// Gets or sets the bool to check ActiveDirectoryDomainName Changed or Not.
        /// </summary>
        public bool IsActiveDirectoryDomainNameChange { get;  set; }
    }
}