using EcommerceAPI.Application.Abstractions.Token;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second, AppUser user)
        {
            Application.DTOs.Token token = new ();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpirationDate = DateTime.UtcNow.AddSeconds(second);

            JwtSecurityToken securityToken = new
            (
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires:token.ExpirationDate,
                notBefore:DateTime.UtcNow,
                signingCredentials:credentials,
                claims: new List<Claim> { new (ClaimTypes.Name, user.UserName)}
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
