namespace TicketManager.Application.Exceptions
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string message) : base(message)
        { }
    }
}
