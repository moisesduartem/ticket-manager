using Microsoft.AspNetCore.Mvc;
using OperationResult;
using TicketManager.Application.Exceptions;

namespace TicketManager.Presentation.Api.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult HandleResult<TResponse>(Result<TResponse> result)
            where TResponse : class
        {
            if (result.Exception is ApplicationLayerException)
            {
                return HandleException(result.Exception);
            }

            return Ok(result.Value);
        }

        protected IActionResult HandleResult<TResponse>(Result<TResponse> result, int httpStatusCode)
            where TResponse : class
        {
            if (result.Exception is ApplicationLayerException)
            {
                return HandleException(result.Exception);
            }

            return StatusCode(httpStatusCode, result.Value);
        }

        private IActionResult HandleException(Exception exception)
        {
            var errorResponse = new { Message = exception.Message };

            return exception switch
            {
                BadRequestException => BadRequest(errorResponse),
                NotFoundException => NotFound(errorResponse),
                _ => StatusCode(StatusCodes.Status500InternalServerError, errorResponse)
            };
        }
    }
}
