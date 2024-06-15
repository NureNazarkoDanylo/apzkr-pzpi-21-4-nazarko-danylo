using WashingMachineManagementApi.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCore.Localizer.Json.Localizer;

namespace WashingMachineManagementApi.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
    private readonly ILogger _logger;
    private readonly IJsonStringLocalizer _localizer;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger, IJsonStringLocalizer localizer)
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new()
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnAuthorizedException), HandleUnAuthorizedException },
            { typeof(ForbiddenException), HandleForbiddenException },
            { typeof(RegistrationException), HandleRegistrationException },
            { typeof(LoginException), HandleLoginException },
            { typeof(RenewAccessTokenException), HandleRenewAccessTokenException },
            { typeof(RevokeRefreshTokenException), HandleRevokeRefreshTokenException },
        };

        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var exceptionType = exception.GetType();

            _logger.LogInformation(
                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Interrupted with {@ExceptionType}.",
                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                Activity.Current?.TraceId.ToString(),
                Activity.Current?.SpanId.ToString(),
                exceptionType);

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(context, exception);
                return;
            }

            await HandleUnhandledException(context, exception);
        }
    }

    private async Task HandleValidationException(HttpContext context, Exception exception)
    {
        var ex = (ValidationException)exception;

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new HttpValidationProblemDetails(ex.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Title = _localizer["ExceptionHandling.ValidationException.Title"],
            Detail = _localizer["ExceptionHandling.ValidationException.Detail"]
        });
    }

    private async Task HandleNotFoundException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
            Title = _localizer["ExceptionHandling.NotFoundException.Title"],
            Detail = _localizer["ExceptionHandling.NotFoundException.Detail"]
        });
    }

    private async Task HandleUnAuthorizedException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-401-unauthorized",
            Title = _localizer["ExceptionHandling.UnAuthorizedException.Title"],
            Detail = _localizer["ExceptionHandling.UnAuthorizedException.Detail"]
        });
    }

    private async Task HandleForbiddenException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status403Forbidden,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-403-forbidden",
            Title = _localizer["ExceptionHandling.ForbiddenException.Title"],
            Detail = exception.Message
        });
    }

    private async Task HandleRegistrationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
            Title = _localizer["ExceptionHandling.RegistrationException.Title"],
            Detail = _localizer["ExceptionHandling.RegistrationException.Detail"]
        });
    }

    private async Task HandleLoginException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
            Title = _localizer["ExceptionHandling.LoginException.Title"],
            Detail = _localizer["ExceptionHandling.LoginException.Detail"]
        });
    }

    private async Task HandleRenewAccessTokenException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
            Title = _localizer["ExceptionHandling.RenewAccessTokenException.Title"],
            Detail = _localizer["ExceptionHandling.RenewAccessTokenException.Detail"]
        });
    }

    private async Task HandleRevokeRefreshTokenException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
            Title = _localizer["ExceptionHandling.RevokeRefreshTokenException.Title"],
            Detail = _localizer["ExceptionHandling.RevokeRefreshTokenException.Detail"]
        });
    }

    private async Task HandleUnhandledException(HttpContext context, Exception exception)
    {
        _logger.LogError(
            "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Unhandled exception.\n{@ExceptionMessage}.\n{@ExceptionStackTrace}.",
            DateTime.UtcNow.ToString("yyyy-MM-dd"),
            DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
            Activity.Current?.TraceId.ToString(),
            Activity.Current?.SpanId.ToString(),
            exception.Message,
            exception.StackTrace);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetailsWithTraceId()
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error",
            Title = _localizer["ExceptionHandling.UnhandledException.Title"],
            Detail = _localizer["ExceptionHandling.UnhandledException.Detail"]
        });
    }

    class ProblemDetailsWithTraceId : ProblemDetails
    {
        public string TraceId { get; init; } = Activity.Current?.TraceId.ToString();
    }
}
