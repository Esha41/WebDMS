using Microsoft.AspNetCore.Authorization;

namespace Intelli.DMS.Api.Basic_Authorization
{
    public class BasicAuthenticationAttribute : AuthorizeAttribute
    {
        public BasicAuthenticationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
