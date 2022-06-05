using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Application.Services
{
    public class TicketsService
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketsRepository.FindAllAsync();
        }
    }
}
