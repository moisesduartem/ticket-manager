using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Services
{
    public interface IAuthTokenService
    {
        public string GenerateFor(User user);
    }
}
