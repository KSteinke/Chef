﻿using Chef_API.Services.TokenAuthentication.Interfaces;
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
        private byte[] secretKey;

        byte[] x = Config.JwtTokenKey;
        public TokenManager()
        {

            _tokenHandler = new JwtSecurityTokenHandler();
            secretKey = x;
        }

        /// <summary>
        /// Generates new JWT
        /// </summary>
        /// <returns></returns>
        public string GenerateToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, "HR") }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = _tokenHandler.WriteToken(token);
            return jwtString;
        }

        public ClaimsPrincipal VeryfiToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}