namespace TicketManager.Application.Exceptions
{
    public class BadRequestException : ApplicationLayerException
    {
        public BadRequestException(string message = "Bad Request") : base(message)
        { }
    }
}
