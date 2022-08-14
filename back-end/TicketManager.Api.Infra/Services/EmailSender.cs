using FluentEmail.Core;
using TicketManager.Api.Core.Services.Email;

namespace TicketManager.Api.Infra.Services
{
    internal class EmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public Task SendAsync(EmailInformation information, CancellationToken cancellationToken)
        {
            var email = _fluentEmail
                     .To(information.To)
                     .Subject(information.Subject);

            if (information.HasTemplateFile)
            {
                var path = $"{Directory.GetCurrentDirectory()}/EmailTemplates/{information.TemplatePath}";
                email.UsingTemplateFromFile(path, information.TemplateModel);
            }

            return email.SendAsync(cancellationToken);
        }
    }
}
