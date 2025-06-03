using Intelli.DMS.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.Views
{
    public class ContractSearchView:ICompanyEntity
    {
        public int Id { get; set; }

        public int RepositoryName { get; set; }


        public string ClientName { get; set; }

        public string DocumentClassName { get; set; }
        public string DocumentTypeName { get; set; }
        public string ContractMetaData { get; set; }
        public string BoxNumber { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CompanyId { get; set; }
    }

}
