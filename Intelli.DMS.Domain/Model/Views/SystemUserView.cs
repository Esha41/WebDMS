using Intelli.DMS.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.Views
{
    public class SystemUserView : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string OutlookEmail { get; set; }
        public bool IsActiveDirectoryUser { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
