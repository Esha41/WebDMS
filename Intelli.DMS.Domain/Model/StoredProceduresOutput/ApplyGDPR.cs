using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.StoredProceduresOutput
{
    public class ApplyGDPR
    {
        public string GDPRStatus { get; set; }
        public int? batchId { get; set; }
        public string requestId { get; set; }
        public int? clientId { get; set; }
        public string clientName { get; set; }
        public DateTime appliedTime { get; set; }
    }
}
