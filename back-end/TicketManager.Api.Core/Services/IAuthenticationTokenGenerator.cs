using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Core.Services
{
    public interface IAuthenticationTokenGenerator
    {
        public string GenerateFor(User user);
    }
}
