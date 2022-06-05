using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.Services;

namespace TicketManager.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
