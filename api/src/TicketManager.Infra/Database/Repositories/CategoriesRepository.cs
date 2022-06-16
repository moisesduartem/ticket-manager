using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Infra.Database.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ILogger<CategoriesRepository> _logger;
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ILogger<CategoriesRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to find all categories in database");
            var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);

            _logger.LogInformation("Returning categories as a enumerable list");
            return categories.AsEnumerable();
        }
    }
}
