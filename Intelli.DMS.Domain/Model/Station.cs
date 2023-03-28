using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class Station
    {
        public Station()
        {
            StationVariables = new HashSet<StationVariable>();
        }

        public int Id { get; set; }
        public string ComputerName { get; set; }
        public bool? Enabled { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<StationVariable> StationVariables { get; set; }
    }
}
