using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class ResourceLanguage
    {
        public ResourceLanguage()
        {
            SystemUsers = new HashSet<SystemUser>();
        }

        public int Id { get; set; }
        public string Language { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<SystemUser> SystemUsers { get; set; }
    }
}
