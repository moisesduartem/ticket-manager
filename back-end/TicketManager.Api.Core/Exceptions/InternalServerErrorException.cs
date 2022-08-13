namespace TicketManager.Api.Core.Exceptions
{
    public class InternalServerErrorException : ApplicationLayerException
    {
        public InternalServerErrorException(string message = "Internal Server Error") : base(message)
        { }
    }
}
