using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManager.Api.Core.Handlers;
using TicketManager.Api.Core.Utilities;
using TicketManager.Api.Core.Validators;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Infra.Database;
using TicketManager.Api.Infra.Database.Repositories;
using TicketManager.Api.Infra.Security.Extensions;
using TicketManager.Api.Infra.Utilities;

namespace TicketManager.Api.Infra.IoC
{
    internal class DependencyResolver
    {
        private readonly IConfiguration Configuration;
        private readonly IServiceCollection Services;

        public DependencyResolver(IConfiguration configuration, IServiceCollection services)
        {
            Configuration = configuration;
            Services = services;
        }

        public void ConfigureMessageBroker()
        {
            Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration.GetConnectionString("RabbitMq"));
                    cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                });
            });
        }

        public void ConfigureAuthentication()
        {
            string jwtSecret = Configuration["Jwt:Secret"];
            Services.AddJsonWebTokenConfiguration(jwtSecret);
        }

        public void ConfigureDatabase()
        {
            string connectionString = Configuration.GetConnectionString("MSSQL");
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void ConfigureDependencyInjection()
        {
            #region Data Access Layer

            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<ITicketsRepository, TicketsRepository>();
            Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            #endregion

            #region Application Layer

            Services.AddScoped<SignInRequestHandler>();
            Services.AddScoped<GetAllTicketsQueryHandler>();
            Services.AddScoped<GetAllCategoriesQueryHandler>();
            
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

        public void ConfigureMediatR()
        {
            Services.AddMediatR(typeof(SignInRequestHandler));
        }
    }
}
