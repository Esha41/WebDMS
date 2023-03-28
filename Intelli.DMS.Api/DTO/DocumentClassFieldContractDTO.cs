using Intelli.DMS.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentClassFieldContractDTO
    {
        public int Id { get; set; }
        public string DocumentClassFieldCode { get; set; }
        public string DocumentClassValue { get; set; }
    }
}
