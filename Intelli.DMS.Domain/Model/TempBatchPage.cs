using System;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    public partial class TempBatchPage
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string KeyFileName { get; set; }
        public string FileName { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
