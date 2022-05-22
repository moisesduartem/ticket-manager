using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Infra.Database;
using TicketManager.Infra.Security.Extensions;

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
    }
}
