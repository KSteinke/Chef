using Chef_Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Chef_Web.Services
{
    public class AuthStateProviderService : AuthenticationStateProvider
    {
        private readonly IAuthManager _jwtAuthManager;
        private ClaimsIdentity _identity { get; set; }
        public AuthStateProviderService(IAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()); 
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_jwtAuthManager.Claims != null)
            {
                _identity = new ClaimsIdentity(_jwtAuthManager.Claims, "jwt");
            }
            else
            {
                _identity = new ClaimsIdentity();
            }
            var user = new ClaimsPrincipal(_identity);
            var state = new AuthenticationState(user);
            return state;
        }
    }
}
