namespace TicketManager.Application.Exceptions
{
    public class InternalServerErrorException : ApplicationLayerException
    {
        public InternalServerErrorException(string message = "Internal Server Error") : base(message)
        { }
    }
}
