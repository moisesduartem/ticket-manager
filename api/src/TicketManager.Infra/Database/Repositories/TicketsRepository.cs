﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Infra.Database.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly ILogger<TicketsRepository> _logger;
        private readonly ApplicationDbContext _context;

        public TicketsRepository(ILogger<TicketsRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task CreateOneAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a new ticket to the database context");
            _context.Tickets.Add(ticket);

            _logger.LogInformation("Saving the changes");
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> FindAllAsync()
        {
            _logger.LogInformation("Starting to find all tickets in database");
            var tickets = await _context.Tickets.AsNoTracking().Include(x => x.Author).Include(x => x.Category).ToListAsync();

            _logger.LogInformation("Returning tickets as a enumerable list");
            return tickets.AsEnumerable();
        }
    }
}
