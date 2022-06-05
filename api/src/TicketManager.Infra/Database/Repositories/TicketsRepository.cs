using Microsoft.EntityFrameworkCore;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Infra.Database.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> FindAllAsync()
        {
            var tickets = await _context.Tickets.AsNoTracking().Include(x => x.Author).Include(x => x.Category).ToListAsync();
            return tickets.AsEnumerable();
        }
    }
}
