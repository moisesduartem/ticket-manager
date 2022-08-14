using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketManager.Api.Core.Domain.DTOs.Tickets;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Repositories;

namespace TicketManager.Api.Infra.Database.Repositories
{
    internal class UserRepository : IUserRepository
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

        public Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Users.AsNoTracking()
                                 .FirstOrDefaultAsync(user => user.Id == id, cancellationToken) as Task<User>;
        }

        public Task<TicketAuthorDTO> GetTicketAuthorByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Users.AsNoTracking()
                                 .Where(user => user.Id == id)
                                 .Select(user => new TicketAuthorDTO
                                 {
                                     Id = user.Id,
                                     Email = user.Email,
                                     Name = user.Name
                                 }).FirstAsync(cancellationToken);
        }
    }
}
