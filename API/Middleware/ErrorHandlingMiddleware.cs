using System.Text.Json;

namespace CRM.API
{
    public sealed class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "Произошла ошибка на сервере",
                    detail = ex.Message
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
