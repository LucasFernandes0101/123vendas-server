using _123vendas.Domain.Base;
using _123vendas.Domain.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Buffers.Text;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = (int)ExceptionStatusCodes.GetExceptionStatusCode(ex);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        _logger.LogError(ex, "An error occurred while processing the request at {Url}", context.Request.Path);

        var result = ex switch
        {
            ValidationException validationEx => CreateValidationErrorResponse(validationEx),
            BaseException baseEx => CreateBaseErrorResponse(baseEx),
            _ => CreateGenericErrorResponse(ex)
        };

        await context.Response.WriteAsync(result);
    }

    private string CreateValidationErrorResponse(ValidationException validationEx)
    {
        var errors = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return JsonConvert.SerializeObject(new
        {
            Message = "Validation failed.",
            Errors = errors
        });
    }

    private string CreateBaseErrorResponse(BaseException baseEx)
    {
        return JsonConvert.SerializeObject(new
        {
            Message = baseEx.Message,
            Details = IsTestEnvironment ? baseEx.StackTrace : string.Empty
        });
    }

    private string CreateGenericErrorResponse(Exception ex)
    {
        return JsonConvert.SerializeObject(new
        {
            Message = "An unexpected error occurred. Please try again later.",
            Details = IsTestEnvironment ? ex.StackTrace : string.Empty
        });
    }

    private static bool IsTestEnvironment =>
        (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Homologation");
}