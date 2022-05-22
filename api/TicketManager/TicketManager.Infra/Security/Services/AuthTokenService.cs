using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TicketManager.Infra.Security.Services
{
    public class AuthTokenService : IAuthTokenService
    {
        private readonly byte[] _secret;
        private readonly int _expireTimeInHours;

        public AuthTokenService(byte[] secret, int expireTimeInHours = 2)
        {
            _secret = secret;
            _expireTimeInHours = expireTimeInHours;
        }

        public string Generate(string id, string name, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(_expireTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
