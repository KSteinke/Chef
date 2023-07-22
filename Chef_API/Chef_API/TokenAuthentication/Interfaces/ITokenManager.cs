using System.Security.Claims;

namespace Chef_API.TokenAuthentication.Interfaces
{
    public interface ITokenManager
    {
        public string GenerateToken();
        ClaimsPrincipal VeryfiToken(string token);
    }
}
