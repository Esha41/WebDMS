using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentTypeRoles
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int SystemRoleId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual SystemRole SystemRole { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
