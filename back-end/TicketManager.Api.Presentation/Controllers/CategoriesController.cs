using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManager.Api.Core.Handlers;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Presentation.Controllers
{
    [Route("api/tickets/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
            return Ok(categories);
        }
    }
}
