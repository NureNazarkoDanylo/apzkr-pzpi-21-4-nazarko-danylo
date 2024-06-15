using MediatR.Behaviors.Authorization;
using MediatR.Behaviors.Authorization.Interfaces;
using WashingMachineManagementApi.Application.Common.Exceptions;

namespace WashingMachineManagementApi.Application.Common.Authorization;

public class CustomUnauthorizedResultHandler : IUnauthorizedResultHandler
    {
        public Task<TResponse> Invoke<TResponse>(AuthorizationResult result)
        {
            throw new ForbiddenException(result.FailureMessage);
        }
    }
