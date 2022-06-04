using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
    }
}
