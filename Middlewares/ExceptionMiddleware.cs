using System.Net;
using App.Utils;
using Newtonsoft.Json;

namespace App.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message;

            // Identify the exception type and assign the appropriate status code
            if (exception is UnauthorizedAccessException) statusCode = (int)HttpStatusCode.Unauthorized;
            if (exception is InvalidOperationException) statusCode = (int)HttpStatusCode.NotFound;

            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            await response.WriteAsync(JsonConvert.SerializeObject(ResponseUtils.ErrorResponse(new {}, message)));
        }
    }
}
