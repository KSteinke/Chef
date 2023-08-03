using System.Security.Claims;

namespace Chef_API.Services.TokenAuthentication.Interfaces
{
    public interface ITokenManager
    {
        public string GenerateToken(string Username);
        ClaimsPrincipal VeryfiToken(string token);
    }
}
