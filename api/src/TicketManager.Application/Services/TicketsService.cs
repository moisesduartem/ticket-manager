using AutoMapper;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Services
{
    public class TicketsService
    {
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(IMapper mapper, ITicketsRepository ticketsRepository)
        {
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
        }

        public async Task<IEnumerable<TicketViewModel>> GetAllAsync()
        {
            var tickets = await _ticketsRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<TicketViewModel>>(tickets);
        }

        public async Task CreateOneAsync(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Ticket>(request);
            await _ticketsRepository.CreateOneAsync(ticket, cancellationToken);
        }
    }
}
