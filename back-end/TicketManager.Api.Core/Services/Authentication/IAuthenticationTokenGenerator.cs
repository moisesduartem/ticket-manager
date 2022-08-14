using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Core.Services.Authentication
{
    public interface IAuthenticationTokenGenerator
    {
        public string GenerateFor(User user);
    }
}
