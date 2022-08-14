using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(SignInRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsFailed)
            {
                return BadRequest(new { Message = GetErrorMessage(result) });
            }

            return Created(result.Value);
        }
    }
}
