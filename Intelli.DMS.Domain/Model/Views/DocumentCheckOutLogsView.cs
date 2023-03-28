using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentCheckOutLogsView
    {
        public int Id { get; set; }
        public int BatchItemId { get; set; }
        public string UserName { get; set; }
        public long CheckedOutAt { get; set; }
        public long CheckedInAt { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
