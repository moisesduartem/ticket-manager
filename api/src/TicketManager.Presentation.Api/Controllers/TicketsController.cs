using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.Services;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : BaseApiController
    {
        private readonly TicketsService _ticketsService;

        public TicketsController(TicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var tickets = await _ticketsService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            ConfigureAuthor(request);

            await _ticketsService.CreateOneAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
