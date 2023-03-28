using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class ClientTag
    {
        [Key()]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? CreatedById { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public virtual Client Client { get; set; }
        public virtual SystemUser CreatedBy { get; set; }
    }
}
