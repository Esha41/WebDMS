using Intelli.DMS.Domain.Model.Core;
using System.ComponentModel.DataAnnotations.Schema;
namespace Intelli.DMS.Domain.Model.Views
{
    public class DocumentReviewView : ICompanyEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string DocumentClassName { get; set; }

        public string DocumentTypeName { get; set; }

        public string ClientName { get; set; }

        public string LastModifiedBy { get; set; }

        public string LastModifiedById { get; set; }
        public int BatchId { get; set; }
        public int CurrentReviewRole { get; set; }
        public bool? IsActive { get; set; }

        public long CreatedAt { get; set; }

        public long UpdatedAt { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckedOutBy { get; set; }
        public string DocumentState { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
    }
}
