using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using TicketManager.Api.Core.Domain.Repositories;
using TicketManager.Api.Core.Handlers;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Services.Email;
using TicketManager.Api.Core.Utilities;
using TicketManager.Api.Core.Validators;
using TicketManager.Api.Infra.Database;
using TicketManager.Api.Infra.Database.Repositories;
using TicketManager.Api.Infra.IoC.Options;
using TicketManager.Api.Infra.Services;

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

        public void ConfigureAuthentication()
        {
            string jwtSecret = Configuration["Jwt:Secret"];
            Services.AddJWTConfiguration(jwtSecret);
        }

        public void ConfigureDatabase()
        {
            string connectionString = Configuration.GetConnectionString("MSSQL");
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void ConfigureDependencyInjection()
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<ITicketsRepository, TicketsRepository>();
            Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<SignInRequestHandler>();
            Services.AddScoped<GetAllTicketsQueryHandler>();
            Services.AddScoped<GetAllCategoriesQueryHandler>();

            Services.AddSingleton<IBcrypt, Bcrypt>();
            Services.AddSingleton<IEmailSender, EmailSender>();
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

        public void ConfigureFluentEmail()
        {
            var emailSenderOptions = Configuration.GetSection("EmailSender").Get<EmailSenderOptions>();

            Services.AddFluentEmail(emailSenderOptions.FromEmail)
                            .AddRazorRenderer()
                            .AddSmtpSender(new SmtpClient(emailSenderOptions.SMTPHost, emailSenderOptions.SMTPPort)
                            {
                                Credentials = new NetworkCredential(
                                    userName: emailSenderOptions.Username,
                                    password: emailSenderOptions.Password),
                                EnableSsl = true

                            });
        }
    }
}
