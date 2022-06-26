using Microsoft.EntityFrameworkCore;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Infra.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _context.Users.AsNoTracking()
                                 .FirstOrDefaultAsync(user => user.Email == email, cancellationToken) as Task<User>;
        }
    }
}
