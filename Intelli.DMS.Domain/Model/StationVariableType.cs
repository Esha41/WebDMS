using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class StationVariableType
    {
        public StationVariableType()
        {
            StationVariables = new HashSet<StationVariable>();
        }

        public int Id { get; set; }
        public string StationVariableTypeName { get; set; }
        public string EnumValue { get; set; }
        public string Comments { get; set; }
        public bool? SupportsGlobal { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual ICollection<StationVariable> StationVariables { get; set; }
    }
}
