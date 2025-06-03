using Intelli.DMS.Shared.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Services.Base
{
    public class BaseService
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        public BaseService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets the current user's Id.
        /// </summary>
        protected int UserId => _accessor.HttpContext.GetUserId();

        /// <summary>
        /// Gets the current user's name.
        /// </summary>
        protected string UserName => _accessor.HttpContext.GetUserEmail();

        /// <summary>
        /// Gets the company ids.
        /// </summary>
        protected List<int> CompanyIds => _accessor.HttpContext.GetCompanyIds();
    }
}
