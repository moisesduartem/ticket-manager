using FluentEmail.Core;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TicketManager.Contracts;

namespace TicketManager.Workers.EmailWorker.Consumers
{
    public class SendTicketCreationEmailConsumer : IConsumer<SendTicketCreationEmail>
    {
        private readonly ILogger<SendTicketCreationEmailConsumer> _logger;
        private readonly IFluentEmail _fluentEmail;

        public SendTicketCreationEmailConsumer(
            ILogger<SendTicketCreationEmailConsumer> logger,
            IFluentEmail fluentEmail)
        {
            _logger = logger;
            _fluentEmail = fluentEmail;
        }

        public async Task Consume(ConsumeContext<SendTicketCreationEmail> context)
        {
            var message = context.Message;
            string templatePath = $"{Directory.GetCurrentDirectory()}/Templates/ticket-created.cshtml";

            try
            {
                _logger.LogInformation(
                    "Sending ticket {TicketId} creation email to {AuthorEmail}",
                    message.TicketId,
                    message.Author.Email
                );

                var email = _fluentEmail
                            .To(message.Author.Email)
                            .Subject("Ticket successfully created!")
                            .UsingTemplateFromFile(templatePath, new
                            {
                                AuthorName = message.Author.Name.Split(" ")[0],
                                TicketId = message.TicketId
                            });

                await email.SendAsync();

                _logger.LogInformation("Sucessfully sent email to {AuthorEmail}", message.Author.Email);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(
                    exception,
                    "Failed to sent ticket {TicketId} creation email to {AuthorEmail}",
                    message.TicketId,
                    message.Author.Email
                );
            }
        }
    }
}
