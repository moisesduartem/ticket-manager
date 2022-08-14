using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicketManager.Api.Infra.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureIoC(this IServiceCollection services, ConfigurationManager configuration)
        {
            var dependencyResolver = new DependencyResolver(configuration, services);
            dependencyResolver.ConfigureDatabase();
            dependencyResolver.ConfigureDependencyInjection();
            dependencyResolver.ConfigureAuthentication();
            dependencyResolver.ConfigureAutoMapper();
            dependencyResolver.ConfigureFluentValidation();
            dependencyResolver.ConfigureMediatR();
            dependencyResolver.ConfigureFluentEmail();
        }
    }
}
