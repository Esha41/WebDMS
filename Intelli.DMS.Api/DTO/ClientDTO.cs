using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Intelli.DMS.Api.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string ClientName => $"{FirstName} {LastName}";

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string JMBG { get; set; }

        public bool? IsActive { get; set; }
        
        public string ExternalId { get; set; }

        public string Reason { get; set; }
        public string ClientStatus { get; set; }
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public bool IsNotValidForTransaction { get; set; }

        public bool IsChecked { get; set; }
        public bool IsJMBGDuplicationCheck { get; set; }
        public bool IsExternalIdDuplicationCheck { get; set; }
        public bool IsNotVaildForChangeStatus { get; set; }

        public List<ClientDocumentListDTO> CustomerDocuments { get; set; }
        public List<ClientTagDTO> Tags { get; set; }
        public List<CompanyCustomFieldDTO> ClientCompanyFieldValues { get; set; }

        public List<ClientRepositoryViewDTO> Repository { get; set; }
        
        /// <summary>
        /// Overriding the ToString to generate string representation of the object
        /// Mainly to be used to logging.
        /// </summary>
        /// <returns>JSON string of teh current object.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
