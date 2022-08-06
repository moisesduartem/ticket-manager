using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TicketManager.Contracts;
using TicketManager.Contracts.Entities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Services
{
    public class TicketsService
    {
        private readonly ILogger<TicketsService> _logger;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IUserRepository _userRepository;

        public TicketsService(
            ILogger<TicketsService> logger,
            IBus bus,
            IMapper mapper,
            ITicketsRepository ticketsRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<TicketViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all the tickets from its repository");
            var tickets = await _ticketsRepository.FindAllAsync(cancellationToken);

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

                _logger.LogInformation("Calling the repository method to create a new ticket");
                await _ticketsRepository.CreateOneAsync(ticket, cancellationToken);

                _logger.LogInformation("Getting author data by its id");
                var author = await _userRepository.GetByIdAsync(request.AuthorId, cancellationToken);

                _logger.LogInformation("Publish an event to send a ticket creation email");
                var command = new SendTicketCreationEmail
                {
                    TicketId = ticket.Id,
                    Author = new TicketAuthor { Id = author.Id, Name = author.Name, Email = author.Email },
                };
                await _bus.Publish(command, cancellationToken);
            }
        }
    }
}
