using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Core.Repositories
{
    public interface ITicketsRepository
    {
        Task<IEnumerable<Ticket>> FindAllAsync(CancellationToken cancellationToken);
        Task CreateOneAsync(Ticket ticket, CancellationToken cancellationToken);
    }
}
