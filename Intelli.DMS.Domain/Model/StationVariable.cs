using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class StationVariable
    {
        public int Id { get; set; }
        public int? StationId { get; set; }
        public int StationVariableTypeId { get; set; }
        public string VariableValue { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        public virtual Station Station { get; set; }
        public virtual StationVariableType StationVariableType { get; set; }
    }
}
