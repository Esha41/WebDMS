using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DataMigrationHistory
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }

    }
}
