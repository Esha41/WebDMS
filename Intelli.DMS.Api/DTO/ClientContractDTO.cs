using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Intelli.DMS.Api.DTO
{
    public class ClientContractDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string JMBG { get; set; }
    }
}
