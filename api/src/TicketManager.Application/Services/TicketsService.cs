using AutoMapper;
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
    }
}
