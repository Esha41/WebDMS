using Intelli.DMS.Domain.Model.Core;
using System.Collections.Generic;

#nullable disable

namespace Intelli.DMS.Domain.Model
{
    /// <summary>
    /// The asp net role.
    /// </summary>
    public partial class SystemRole : ICompanyEntity
    {
        public void ReduceResponseSize()
        {
            this.Company.SystemRoles = null;
            this.Company.DocumentsPerCompanies = null;
            this.Company.UserCompanies = null;
            this.SystemUserRoles = null;
        }
    }

}
