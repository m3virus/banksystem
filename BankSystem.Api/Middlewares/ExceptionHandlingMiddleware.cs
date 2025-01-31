using System.Net;
using System.Text.Json;
using Azure.Core;
using BankSystem.Api.Models;


namespace BankSystem.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = exception.Message,
            Details = exception.InnerException?.Message
        };

        context.Response.ContentType = ContentType.ApplicationJson.ToString();
        context.Response.StatusCode = response.StatusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

