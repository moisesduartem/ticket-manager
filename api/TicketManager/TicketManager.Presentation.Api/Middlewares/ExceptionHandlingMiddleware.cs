using System.Net;

namespace TicketManager.Presentation.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {
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
