using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentCheckOutView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string ClientName { get; set; }
        public int Status { get; set; }
        public string DocumentState { get; set; }
        public bool? IsActive { get; set; }
        public int VersionId { get; internal set; }
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        public long UpdatedAt { get; set; }
        public int CompanyId { get; set; }
    }
}
