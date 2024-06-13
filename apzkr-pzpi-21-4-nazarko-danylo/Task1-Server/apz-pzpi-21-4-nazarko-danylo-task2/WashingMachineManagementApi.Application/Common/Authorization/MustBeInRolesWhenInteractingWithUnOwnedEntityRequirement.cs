using AspNetCore.Localizer.Json.Localizer;
using MediatR.Behaviors.Authorization;

namespace WashingMachineManagementApi.Application.Common.Authorization;

public class MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement : IAuthorizationRequirement
{
    public required string UserId { get; init; } = default!;
    public required ICollection<string> UserRoles { get; init; } = default!;

    public required string RequiredUserId { get; init; } = default!;
    public required ICollection<string> RequiredRoles { get; init; } = default!;

    class MustBeInRolesWhenInteractingWithUnOwnedEntityRequirementHandler
        : IAuthorizationHandler<MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement>
    {
        private readonly IJsonStringLocalizer _localizer;

        public MustBeInRolesWhenInteractingWithUnOwnedEntityRequirementHandler(IJsonStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<AuthorizationResult> Handle(
            MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement request,
            CancellationToken cancellationToken)
        {
            var isUserOwner = request.UserId == request.RequiredUserId;
            var isUserInRequiredRoles = request.UserRoles.Any(ur => request.RequiredRoles.Contains(ur));

            if (!isUserOwner && !isUserInRequiredRoles)
            {
                return AuthorizationResult.Fail(
                    String.Format(
                        _localizer["AuthorizationRequirements.MustBeInRolesWhenInteractingWithUnOwnedEntityRequirementHandler"],
                        $"'{String.Join("', '", request.RequiredRoles)}'"));
            }

            return AuthorizationResult.Succeed();
        }
    }
}
