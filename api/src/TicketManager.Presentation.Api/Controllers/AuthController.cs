using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.Services;
using TicketManager.Shared.DTOs.UserAccess;

namespace TicketManager.Presentation.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(SignInCommand request, CancellationToken cancellationToken)
            => HandleResult(await _authService.SignInAsync(request), httpStatusCode: 201);
    }
}
