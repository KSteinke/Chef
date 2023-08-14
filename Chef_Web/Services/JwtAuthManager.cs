using Chef_Web.Extentions;
using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace Chef_Web.Services
{
    public class JwtAuthManager : IAuthManager
    {
        

        public string? AuthToken { get; set; }
        public IEnumerable<Claim>? Claims { get; set; } //TO DO - nie da się tak trzymać claimu, trzeba na bierząco odczytywać z pamięci https://stackoverflow.com/questions/59909081/blazor-client-web-assembly-authenticationstate-updates-only-after-page-reloadi

        public void LogUser(string jwt)
        {
            AuthToken = jwt;
            Claims = ExtentionMethods.ParseClaimsFromJwt(jwt);
        }

        public void LogoutUser()
        {
            AuthToken = null;
            Claims = null;
        }
    }
}
