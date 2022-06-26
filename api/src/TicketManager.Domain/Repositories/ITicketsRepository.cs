using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface ITicketsRepository
    {
        Task<IEnumerable<Ticket>> FindAllAsync(CancellationToken cancellationToken);
        Task CreateOneAsync(Ticket ticket, CancellationToken cancellationToken);
    }
}
