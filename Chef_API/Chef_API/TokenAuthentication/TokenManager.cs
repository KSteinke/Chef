﻿using Chef_API.TokenAuthentication.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chef_API.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey = Config.JwtTokenKey;

        public TokenManager()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            
        }

        /// <summary>
        /// Generates new JWT
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, "XXXX - Here user info!")}), //TODO - add registration featuers
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }

        public ClaimsPrincipal VeryfiToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
