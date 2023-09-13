
using System.Net;
using System.Text.Json;
using PartyRoom.Core.DTOs.Error;

namespace PartyRoom.WebAPI.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
            }
            catch(InvalidCastException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);

            }
            catch
            {
                await HandleExceptionAsync(httpContext, "Неизвестная ошибка", HttpStatusCode.BadRequest);

            }
        }
        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            _logger.LogError(message);
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            ErrorDto error = new()
            {
                Message = message,
                StatusCode = (int)statusCode
            };

            var result = JsonSerializer.Serialize(error);
            await response.WriteAsJsonAsync(result);
        }
    }
}

