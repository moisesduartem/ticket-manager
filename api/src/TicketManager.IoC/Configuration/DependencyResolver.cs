using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Application.Services;
using TicketManager.Application.Utilities;
using TicketManager.Application.Validators;
using TicketManager.Domain.Repositories;
using TicketManager.Infra.Database;
using TicketManager.Infra.Database.Repositories;
using TicketManager.Infra.Security.Extensions;
using TicketManager.Infra.Utilities;

namespace TicketManager.IoC.Configuration
{
    public class DependencyResolver
    {
        private readonly IConfiguration Configuration;
        private readonly IServiceCollection Services;

        public DependencyResolver(IConfiguration configuration, IServiceCollection services)
        {
            Configuration = configuration;
            Services = services;
        }

        public void ConfigureAuthentication()
        {
            string jwtSecret = Configuration["Jwt:Secret"];
            Services.AddJsonWebTokenConfiguration(jwtSecret);
        }

        public void ConfigureDatabase()
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void ConfigureDependencyInjection()
        {
            #region Data Access Layer

            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<ITicketsRepository, TicketsRepository>();

            #endregion

            #region Application Layer

            Services.AddScoped<AuthService>();
            Services.AddScoped<TicketsService>();
            
            Services.AddScoped<IBcrypt, Bcrypt>();

            #endregion

        }

        public void ConfigureAutoMapper()
        {
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void ConfigureFluentValidation()
        {
            Services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignInRequestValidator>());
        }
    }
}
