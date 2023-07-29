using Chef_Web.Extentions;
using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Chef_Web.Services
{
    public class AuthStateProviderService : AuthenticationStateProvider
    {
        private readonly ITokenManager _tokenManager;
        private readonly AuthenticationState _anonymous;
        public List<String> claims { get; set; }
        public AuthStateProviderService(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenManager.GetTokenAsync();
            var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ExtentionMethods.ParseClaimsFromJwtSecond(token), "jwtAuthType");

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            return state;
        }

        public void NotifyUserAuthentication(string token)
        {
            var authUser = new ClaimsPrincipal(new ClaimsIdentity(ExtentionMethods.ParseClaimsFromJwtSecond(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }



    }
}
