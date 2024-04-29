using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Core.Entities;
using Shop.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Infrastructure.Repository
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"]!));
        }

        public string CreateToken(AppUser appUser)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>() {
                new (JwtRegisteredClaimNames.Email, appUser.Email!),
                new (JwtRegisteredClaimNames.GivenName, appUser.DisplayName!),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                Issuer = _configuration["token:issuer"],
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
