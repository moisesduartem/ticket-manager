using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Services.Authentication;

namespace TicketManager.Api.Infra.Services
{
    internal class JWTTokenGenerator : IAuthenticationTokenGenerator
    {
        private readonly byte[] _secret;
        private readonly int _expireTimeInHours;

        public JWTTokenGenerator(byte[] secret, int expireTimeInHours = 6)
        {
            _secret = secret;
            _expireTimeInHours = expireTimeInHours;
        }

        public string GenerateFor(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = DateTime.UtcNow.AddHours(_expireTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
