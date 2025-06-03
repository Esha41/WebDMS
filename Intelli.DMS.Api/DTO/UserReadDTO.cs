using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    /// <summary>
    /// The user read DTO.
    /// </summary>
    public class UserReadDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// 
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// 
        [Required]
        public string FullName { get; set; }

        public string Company { get; set; }
        public string OutlookEmail { get; set; }
        public bool IsOutlookEmailChanged { get; set; }
        public bool IsActiveDirectoryUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public List<int> CompanyIds { get; set; }

        /// <summary>
        /// Gets or sets the user's company.
        /// </summary>
       // public CompanyDTO Company { get; set; }

        /// <summary>
        /// Gets or sets the role ids.
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// Gets or sets the country ids.
        /// </summary>
        public List<int> CountryIds { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        public IEnumerable<RoleDTO> Roles { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        public IEnumerable<CountryDTO> Countries { get; set; }

        public IEnumerable<UserCompany> UserCompanies { get; set; }

        public IEnumerable<CompanyDTO> Companies { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether user must change password or not.
        /// This flag will be raised when it is set that User should change password after some days.
        /// </summary>
        public bool MustChangePassword { get; set; } = false;
        public bool IsUserChangedPassword { get; set; } 

        public List<string> CompanyNames { get; set; } = new();

        public void setAssociatedCompanies()
        {
            if(this.UserCompanies != null)
            {
                foreach(var userCompany in this.UserCompanies)
                {
                    this.CompanyNames.Add(userCompany?.Company?.CompanyName);
                }
            }

            this.UserCompanies = null;
        }
    }
}
