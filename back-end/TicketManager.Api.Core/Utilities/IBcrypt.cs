namespace TicketManager.Api.Core.Utilities
{
    public interface IBcrypt
    {
        bool Verify(string text, string hash);
        string Hash(string input);
    }
}
