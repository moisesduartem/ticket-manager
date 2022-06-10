using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface ITicketsRepository
    {
        Task<IEnumerable<Ticket>> FindAllAsync();
        Task CreateOneAsync(Ticket ticket, CancellationToken cancellationToken);
    }
}
