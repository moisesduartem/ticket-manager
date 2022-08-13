using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using TicketManager.Contracts;
using TicketManager.Contracts.Entities;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Core.Handlers
{
    public class CreateTicketRequestHandler : IRequestHandler<CreateTicketRequest, Result>
    {
        private readonly ILogger<CreateTicketRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IUserRepository _userRepository;

        public CreateTicketRequestHandler(
            ILogger<CreateTicketRequestHandler> logger,
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

        public async Task<Result> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Title"] = request.Title,
                ["Description"] = request.Description,
                ["AuthorId"] = request.AuthorId,
                ["CategoryId"] = request.CategoryId,
            }))
            {
                _logger.LogInformation("Mapping request object");
                var ticket = _mapper.Map<Ticket>(request);

                _logger.LogInformation("Attempting to create a new ticket");
                await _ticketsRepository.CreateOneAsync(ticket, cancellationToken);

                _logger.LogInformation("Successfully created a new ticket with Id={TicketId}", ticket.Id);
                _logger.LogInformation("Getting author data by its id");
                var author = await _userRepository.GetByIdAsync(request.AuthorId, cancellationToken);

                _logger.LogInformation(
                    "Publishing event to notify the ticket creation to Email={AuthorEmail}",
                    author.Email
                );
                var command = GetSendEmailCommand(ticket, author);
                await _bus.Publish(command, cancellationToken);

                return Result.Success();
            }
        }

        private SendTicketCreationEmail GetSendEmailCommand(Ticket ticket, User author)
        {
            return new SendTicketCreationEmail
            {
                TicketId = ticket.Id,
                Author = new TicketAuthor { Id = author.Id, Name = author.Name, Email = author.Email },
            };
        }
    }
}
