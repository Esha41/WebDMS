using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Country
    {
        public Country()
        {
            SystemUserCountries = new HashSet<SystemUserCountry>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public string Code2D { get; set; }
        public string Code3D { get; set; }
        public string MobileCode { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<SystemUserCountry> SystemUserCountries { get; set; }
    }
}
