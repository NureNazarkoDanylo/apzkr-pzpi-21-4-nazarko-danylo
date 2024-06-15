using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using WashingMachineManagementApi.Application.Common.IServices;

namespace WashingMachineManagementApi.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ISessionUserService _sessionUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ISessionUserService sessionUserService)
    {
        _logger = logger;
        _sessionUserService = sessionUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Handling {@RequestName} by user with Email: {@UserEmail} and UserId: {@UserId}.",
            DateTime.UtcNow.ToString("yyyy-MM-dd"),
            DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
            Activity.Current?.TraceId.ToString(),
            Activity.Current?.SpanId.ToString(),
            typeof(TRequest).Name,
            _sessionUserService.Email,
            _sessionUserService.Id);

        var response = await next();

        _logger.LogInformation(
            "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Handled {@RequestName} by user with Email: {@UserEmail} and UserId: {@UserId}.",
            DateTime.UtcNow.ToString("yyyy-MM-dd"),
            DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
            Activity.Current?.TraceId.ToString(),
            Activity.Current?.SpanId.ToString(),
            typeof(TRequest).Name,
            _sessionUserService.Email,
            _sessionUserService.Id);

        return response;
    }
}
