using Microsoft.EntityFrameworkCore;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Infra.Database.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
            return categories.AsEnumerable();
        }
    }
}
