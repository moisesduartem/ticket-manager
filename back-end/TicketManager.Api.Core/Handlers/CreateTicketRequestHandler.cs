using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Domain.Repositories;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Requests;
using TicketManager.Api.Core.Services.Email;

namespace TicketManager.Api.Core.Handlers
{
    public class CreateTicketRequestHandler : IRequestHandler<CreateTicketRequest, Result>
    {
        private readonly ILogger<CreateTicketRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketRequestHandler(
            ILogger<CreateTicketRequestHandler> logger,
            IMapper mapper,
            ITicketsRepository ticketsRepository,
            IUserRepository userRepository,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
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
                await Task.WhenAll(
                    _ticketsRepository.InsertOneAsync(ticket, cancellationToken),
                    _unitOfWork.SaveChangesAsync(cancellationToken)
                );

                _logger.LogInformation("Successfully created a new ticket with Id={TicketId}", ticket.Id);
                
                _logger.LogInformation("Getting author email and name by Id={AuthorId}", request.AuthorId);
                var author = await _userRepository.GetTicketAuthorByIdAsync(request.AuthorId, cancellationToken);

                var email = new EmailInformation(
                        to: author.Email,
                        subject: "Ticket successfully created!",
                        templatePath: "successfully-created-a-new-ticket.cshtml",
                        templateModel: new
                        {
                            AuthorName = author.Name.Split(" ")[0],
                            TicketId = ticket.Id
                        });

                _logger.LogInformation(
                    "Sending TicketId={TicketId} creation email to Email={AuthorEmail}",
                    ticket.Id,
                    author.Email);
                await _emailSender.SendAsync(email, cancellationToken);

                _logger.LogInformation("Email successfully sent to Email={AuthorEmail}", author.Email);

                return Result.Ok();
            }
        }
    }
}
