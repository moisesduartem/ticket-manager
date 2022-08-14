namespace TicketManager.Api.Infra.IoC.Options
{
    public class EmailSenderOptions
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string FromEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
