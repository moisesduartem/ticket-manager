using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketManager.Api.Core.Services.Authentication;

namespace TicketManager.Api.Infra.Services
{
    internal static class JWTExtensions
    {
        public static void AddJWTConfiguration(this IServiceCollection services, string jwtSecret)
        {
            var secret = Encoding.ASCII.GetBytes(jwtSecret);

            services.AddScoped<IAuthenticationTokenGenerator>(_ => new JWTTokenGenerator(secret));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
