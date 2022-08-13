using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery(), cancellationToken);
            return Ok(tickets);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            ConfigureAuthor(request);
            
            await _mediator.Send(request, cancellationToken);
            
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
