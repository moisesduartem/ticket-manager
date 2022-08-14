namespace TicketManager.Api.Core.Services.Email
{
    public class EmailInformation
    {
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string? TemplatePath { get; private set; }
        public object TemplateModel { get; private set; }

        public EmailInformation(string to, string subject)
        {
            To = to;
            Subject = subject;
            TemplatePath = null;
            TemplateModel = new { };
        }

        public EmailInformation(string to, string subject, string templatePath)
        {
            To = to;
            Subject = subject;
            TemplatePath = templatePath;
            TemplateModel = new { };
        }

        public EmailInformation(string to, string subject, string templatePath, object templateModel)
        {
            To = to;
            Subject = subject;
            TemplatePath = templatePath;
            TemplateModel = templateModel;
        }

        public bool HasTemplateFile => TemplatePath != null;
    }
}
