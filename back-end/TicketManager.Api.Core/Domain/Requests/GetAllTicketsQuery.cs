using MediatR;
using TicketManager.Api.Core.Domain.DTOs.Tickets;

namespace TicketManager.Api.Core.Requests
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDTO>>
    {
    }
}
