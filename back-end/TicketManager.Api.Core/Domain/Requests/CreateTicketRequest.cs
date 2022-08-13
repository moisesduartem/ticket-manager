using MediatR;
using OperationResult;

namespace TicketManager.Api.Core.Requests
{
    public class CreateTicketRequest : IRequest<Result>, IAuthorRequest
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
