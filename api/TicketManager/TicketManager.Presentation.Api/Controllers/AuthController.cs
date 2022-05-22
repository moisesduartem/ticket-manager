using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.Services;
using TicketManager.Shared.DTOs.UserAccess;

namespace TicketManager.Presentation.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly UserAccessService _userAccessService;

        public AuthController(UserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(SignInCommand request, CancellationToken cancellationToken)
        {
            return HandleResult(await _userAccessService.SignInAsync(request), httpStatusCode: 201);
        }
    }
}
