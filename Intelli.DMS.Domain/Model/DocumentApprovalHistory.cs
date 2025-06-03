using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentApprovalHistory
    {
        public int Id { get; set; }
        public string BatchItemReference { get; set; }

        public int RoleId { get; set; }
        public bool Approved { get; set; }
        public string Approvedby { get; set; }

        public long CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public long UpdatedAt { get; set; }

        public virtual SystemRole Role { get; set; }

    }
}
