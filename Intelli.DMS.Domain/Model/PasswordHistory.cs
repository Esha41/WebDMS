using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class PasswordHistory
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public long ChangedAt { get; set; }
        public string PasswordHash { get; set; }

        public virtual SystemUser SystemUser { get; set; }
    }
}
