using Shop.API.Errors;
using System.Net;
using System.Text.Json;

namespace Shop.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment, JsonSerializerOptions jsonSerializerOptions)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Este error procede de una excepción Middleware: {ex.Message} !");
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = _hostEnvironment.IsDevelopment()
                ? new APIException(statusCode, ex.Message, ex.StackTrace!.ToString())
                : new APIException(statusCode);

            var json = JsonSerializer.Serialize(response, _jsonSerializerOptions);
            await context.Response.WriteAsync(json);
        }
    }
}
