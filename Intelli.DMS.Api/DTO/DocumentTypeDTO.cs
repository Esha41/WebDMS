using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class DocumentTypeDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DocumentTypeName { get; set; }
        [Required]
        public string DocumentTypeCode { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public bool IsDocumentTypeChanged { get; set; }
        public bool IsDocumentCodeChanged { get; set; }
        public bool IsRolesModifiable { get; set; }
        public List<DocumrntTypeRoleDTO> RoleLists { get; set; }
        public List<DocumrntTypeRoleDTO> RoleAccessLists { get; set; }

        public bool? IsActive { get; set; }


    }
}