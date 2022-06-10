using System.Net;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Presentation.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = new
                {
                    Message = "An unexpected error occurred while trying to process the request."
                };
                await response.WriteAsJsonAsync(result);
            }
        }
    }
}
