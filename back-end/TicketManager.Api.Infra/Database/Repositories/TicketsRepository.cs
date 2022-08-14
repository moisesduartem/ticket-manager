using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Repositories;

namespace TicketManager.Api.Infra.Database.Repositories
{
    internal class TicketsRepository : ITicketsRepository
    {
        private readonly ILogger<TicketsRepository> _logger;
        private readonly ApplicationDbContext _context;

        public TicketsRepository(ILogger<TicketsRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InsertOneAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a new ticket to the database context");
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task<IEnumerable<Ticket>> FindAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to find all tickets in database");
            var tickets = await _context.Tickets.AsNoTracking()
                                                .Include(x => x.Author)
                                                .Include(x => x.Category)
                                                .ToListAsync(cancellationToken);

            _logger.LogInformation("Returning tickets as a enumerable list");
            return tickets.AsEnumerable();
        }
    }
}
