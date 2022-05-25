using TicketManager.Application.Utilities;

namespace TicketManager.Infra.Utilities
{
    public class Bcrypt : IBcrypt
    {
        public bool Verify(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }

        public string Hash(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
    }
}
