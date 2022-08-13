namespace TicketManager.Api.Core.Exceptions
{
    public class NotFoundException : ApplicationLayerException
    {
        public NotFoundException(string message = "Not found") : base(message)
        { }
    }
}
