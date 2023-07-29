using Chef_Web.Services.Contracts;
using Newtonsoft.Json.Linq;

namespace Chef_Web.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly ICookieManager _cookieManager;
        public TokenManager(ICookieManager cookieManager)
        {
            _cookieManager = cookieManager;
        }
        
        public async Task SetTokenAsync(string token)
        {
            await _cookieManager.SetValueAsync("Token", token);
        }
        
        public async Task<string> GetTokenAsync()
        {
            var token = await _cookieManager.GetValueAsync("Token");
            if(token == "")
            {
                return null;
            }
            else if (token.StartsWith("Token="))
            {
                // Jeśli tak, zwróć resztę ciągu po "Token="
                return token.Substring("Token=".Length);
            }
            else
            {
                // Jeśli nie, zwróć oryginalny ciąg
                return token;
            }

        }

        public async Task DisposeToken()
        {
            await _cookieManager.SetValueAsync("Token", "");
        }
    }
}
