using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Presentation.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected void ConfigureAuthor(IAuthorRequest request)
        {
            request.AuthorId = int.Parse(GetLoggedUserId());
        }

        protected string GetLoggedUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected IActionResult Created(object content)
        {
            return StatusCode(StatusCodes.Status201Created, content);
        }

        protected IActionResult Created()
        {
            return StatusCode(StatusCodes.Status201Created);
        }

        protected string GetErrorMessage<T>(Result<T> result) where T : class
        {
            return result.Errors.First().Message;
        }
    }
}
