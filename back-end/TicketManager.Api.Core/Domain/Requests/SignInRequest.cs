using MediatR;
using OperationResult;
using TicketManager.Api.Core.Domain.DTOs.Auth;

namespace TicketManager.Api.Core.Requests
{
    public class SignInRequest : IRequest<Result<SignInDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
