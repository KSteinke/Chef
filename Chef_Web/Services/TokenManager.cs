using Chef_Web.Services.Contracts;

namespace Chef_Web.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly ICookieManager _cookieManager;

        public TokenManager(ICookieManager cookieManager)
        {
            _cookieManager = cookieManager;
        }

        public async Task WriteToken(string token)
        {
            //TO DO - zastanowić się nad logiką biznesową czy TokenManager powinien mieć stan logowania
            await _cookieManager.SetValueAsync("Authorization", token);

        }

        public async Task<string> ReadToken()
        {
            string token = await _cookieManager.GetValueAsync("Authorization");
            return token;
        }
    }
}
