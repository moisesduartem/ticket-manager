using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketManager.Api.Core.Services;
using TicketManager.Api.Infra.Security.Services;

namespace TicketManager.Api.Infra.Security.Extensions
{
    internal static class ServiceCollectionExtension
    {
        public static void AddJsonWebTokenConfiguration(this IServiceCollection services, string jwtSecret)
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
