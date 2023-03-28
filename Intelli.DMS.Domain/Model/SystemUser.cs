using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            ColumnPreferences = new HashSet<ColumnPreference>();
            PasswordHistories = new HashSet<PasswordHistory>();
            SystemUserCountries = new HashSet<SystemUserCountry>();
            SystemUserRoles = new HashSet<SystemUserRole>();
            UserCompanies = new HashSet<UserCompany>();
            UserPreferences = new HashSet<UserPreference>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required]
        [StringLength(256)]
        public string OutlookEmail { get; set; }
        public string Jmbg { get; set; }
        public bool? IsActive { get; set; }
        public bool IsActiveDirectoryUser { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual UserSession UserSession { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<ColumnPreference> ColumnPreferences { get; set; }
        public virtual ICollection<PasswordHistory> PasswordHistories { get; set; }
        public virtual ICollection<SystemUserCountry> SystemUserCountries { get; set; }
        public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; }
    }
}
