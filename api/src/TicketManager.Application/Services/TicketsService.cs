using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Services
{
    public class TicketsService
    {
        private readonly ILogger<TicketsService> _logger;
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(ILogger<TicketsService> logger, IMapper mapper, ITicketsRepository ticketsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
        }

        public async Task<IEnumerable<TicketViewModel>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all the tickets from its repository");
            var tickets = await _ticketsRepository.FindAllAsync();

            _logger.LogInformation("Returning the mapped result");
            return _mapper.Map<IEnumerable<TicketViewModel>>(tickets);
        }

        public async Task CreateOneAsync(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Title"] = request.Title,
                ["Description"] = request.Description,
                ["AuthorId"] = request.AuthorId,
                ["CategoryId"] = request.CategoryId,
            }))
            {
                _logger.LogInformation("Starting to map the received request object into a Ticket instance");
                var ticket = _mapper.Map<Ticket>(request);

                _logger.LogInformation($"Calling the repository method to create a new ticket");
                await _ticketsRepository.CreateOneAsync(ticket, cancellationToken);
            }
        }
    }
}
