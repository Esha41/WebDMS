using Intelli.DMS.Domain.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Model.Views
{
    public class DocumentSearchView:ICompanyEntity
    {
        public int Id { get; set; }

        public int RepositoryName { get; set; }

        public string FileName { get; set; }

        public string ClientName { get; set; }

        public int FileVersion { get; set; }
        public string DocumentClassName { get; set; }
        public string DocumentTypeName { get; set; }
        public string LastModifiedById { get; set; }
        public string DocumentState { get; set; }

        public int FileStatus { get; set; }

        public string ClientStatus { get; set; }
        public string CurrentReviewRole { get; set; }
        public long? ExpirationDate { get; set; }

        public long CreatedOn { get; set; }

        public long LastModifiedOn { get; set; }
        public string DocumentMetaData { get; set; }
        public string ClientMetaData { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckedOutBy { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CompanyId { get; set; }
    }

}
