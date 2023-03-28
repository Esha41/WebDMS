using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model
{
    public partial class DocumentsCheckedOutLog
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public long CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        public long UpdatedAt { get; set; }

        public int BatchItemId { get; set; }

        public BatchItem BatchItem { get; set; }

        public int SystemUserId { get; set; }

        public SystemUser SystemUser { get; set; }

        public long CheckedOutAt { get; set; }

        public long CheckedInAt { get; set; }
    }
}
