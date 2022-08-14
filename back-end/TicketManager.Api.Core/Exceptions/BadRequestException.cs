namespace TicketManager.Api.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Bad request") : base(message)
        { }
    }
}
