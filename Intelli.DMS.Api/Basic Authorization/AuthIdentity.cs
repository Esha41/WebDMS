using System.Security.Principal;

namespace Intelli.DMS.Api.Basic_Authorization
{
    public class AuthIdentity: IIdentity
    {
        public AuthIdentity(string authenticationType, bool isAuthenticated, string name)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        public string Name { get; }
    }
}
