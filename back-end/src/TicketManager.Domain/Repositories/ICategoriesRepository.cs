using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken);
    }
}
