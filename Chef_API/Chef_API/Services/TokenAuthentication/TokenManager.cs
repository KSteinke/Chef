using Chef_API.Services.TokenAuthentication.Interfaces;
using Chef_Models.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chef_API.Services.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _config;



        //byte[] TokenKey = Config.JwtTokenKey;
        public TokenManager(IConfiguration config)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _config = config;
        }

        
        public string GenerateToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userName)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetValue<string>("TokenKeys:JwtTokenKey"))),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = _tokenHandler.WriteToken(token);
            return jwtString;
        }

        
    }
}
