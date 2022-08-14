using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.Repositories;

namespace TicketManager.Api.Infra.Database.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving changes in database");
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
