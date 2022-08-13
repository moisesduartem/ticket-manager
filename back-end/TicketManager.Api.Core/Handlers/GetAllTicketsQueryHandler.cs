using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.DTOs.Tickets;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Core.Handlers
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDTO>>
    {
        private readonly ILogger<GetAllTicketsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;

        public GetAllTicketsQueryHandler(
            ILogger<GetAllTicketsQueryHandler> logger,
            IMapper mapper,
            ITicketsRepository ticketsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all tickets");
            var tickets = await _ticketsRepository.FindAllAsync(cancellationToken);

            _logger.LogInformation("Mapping tickets results, Total={Count}", tickets.Count());
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }
    }
}
