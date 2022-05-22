using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Infra.Database;
using TicketManager.Shared.Infra;
using TicketManager.Shared.Options;

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

        public void ConfigureDatabase()
        {
            Services.Configure<ConnectionStringsOptions>((_) => Configuration.GetSection("ConnectionStrings"));
            Services.AddDbContext<ApplicationDbContext>();
            Services.AddSingleton<IDatabaseContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
