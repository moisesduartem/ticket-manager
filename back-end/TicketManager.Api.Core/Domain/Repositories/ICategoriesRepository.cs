using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Core.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken);
    }
}
