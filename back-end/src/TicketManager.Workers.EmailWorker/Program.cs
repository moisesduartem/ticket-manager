using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TicketManager.Workers.EmailWorker.Consumers;
using TicketManager.Workers.EmailWorker.Options;

namespace TicketManager.Workers.EmailWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, cfg) =>
                    cfg.WriteTo.Console(outputTemplate: "[{Timestamp:o} {Level:u3}] => {Message:lj} {Properties:j} {NewLine} {Exception}"))
                .ConfigureServices((hostContext, services) =>
                {
                    var emailSenderOptions = hostContext.Configuration.GetSection("EmailSender")
                                                                      .Get<EmailSenderOptions>();

                    services.AddFluentEmail(emailSenderOptions.FromEmail)
                            .AddRazorRenderer()
                            .AddSmtpSender(new SmtpClient(emailSenderOptions.SMTPHost, emailSenderOptions.SMTPPort)
                            {
                                Credentials = new NetworkCredential(
                                    userName: emailSenderOptions.Username,
                                    password: emailSenderOptions.Password),
                                EnableSsl = true

                            });

                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        x.AddConsumer<SendTicketCreationEmailConsumer>();

                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(hostContext.Configuration.GetConnectionString("RabbitMq"));

                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                });
    }
}
