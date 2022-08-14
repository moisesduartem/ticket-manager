namespace TicketManager.Api.Core.Services.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailInformation email, CancellationToken cancellationToken);
    }
}
