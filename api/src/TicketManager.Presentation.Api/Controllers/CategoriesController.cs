using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.Services;

namespace TicketManager.Presentation.Api.Controllers
{
    [Route("api/tickets/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : BaseApiController
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var categories = await _categoriesService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }
    }
}
