using _123vendas.Domain.Base;
using _123vendas.Domain.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace _123vendas_server.v1.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = (int)(ex is BaseException baseEx ?
                                ExceptionStatusCodes.GetExceptionStatusCode(baseEx) :
                                HttpStatusCode.InternalServerError);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        _logger.LogError(ex, "An error occurred while processing the request.");

        if(ex is ValidationException validationEx)
        {
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                Message = "Validation failed.",
                Errors = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            });
            return context.Response.WriteAsync(result);
        }

        if (ex is not BaseException && !IsTestEnvironment)
        {
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                Message = "An unexpected error occurred. Please try again later."
            });
            return context.Response.WriteAsync(result);
        }

        var detailedResult = JsonConvert.SerializeObject(new
        {
            StatusCode = statusCode,
            Message = ex.Message,
            Details = IsTestEnvironment ? ex.StackTrace : string.Empty
        });

        return context.Response.WriteAsync(detailedResult);
    }

    private static bool IsTestEnvironment =>
        (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Homologation");
}