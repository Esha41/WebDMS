using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Intelli.DMS.Domain.OLDDBModel
{
    public partial class PelaXr
    {
        public decimal? PelXaas { get; set; }
        public string PelProf { get; set; }
        public string Pel_Idno { get; set; }
        /// <summary>
        /// Last Name.
        /// </summary>
        public string PelEpon { get; set; } //last name
        /// <summary>
        /// First Name.
        /// </summary>
        public string PelOnom { get; set; } //first name
        [NotMapped]
        public int? CompanyId { get; set; } = null;
        [NotMapped]
        public long? CreatedAt { get; set; } = null;
        [NotMapped]
        public long? UpdatedAt { get; set; } = null;
        [NotMapped]
        public bool? IsActive { get; set; } = null;
    }
}
