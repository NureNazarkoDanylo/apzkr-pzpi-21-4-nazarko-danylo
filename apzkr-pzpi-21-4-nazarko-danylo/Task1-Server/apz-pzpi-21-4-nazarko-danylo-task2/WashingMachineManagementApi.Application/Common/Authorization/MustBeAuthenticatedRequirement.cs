using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Exceptions;

namespace WashingMachineManagementApi.Application.Common.Authorization;

public class MustBeAuthenticatedRequirement : IAuthorizationRequirement
{
    public required bool IsAuthenticated { get; init; } = default!;

    class MustBeAuthenticatedRequirementHandler : IAuthorizationHandler<MustBeAuthenticatedRequirement>
    {
        public async Task<AuthorizationResult> Handle(MustBeAuthenticatedRequirement request, CancellationToken cancellationToken)
        {
            if (!request.IsAuthenticated)
            {
                throw new UnAuthorizedException();
            }

            return AuthorizationResult.Succeed();
        }
    }
}
