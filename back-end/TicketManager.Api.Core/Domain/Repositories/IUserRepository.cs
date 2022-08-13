using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
