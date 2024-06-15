using AspNetCore.Localizer.Json.Localizer;
using MediatR.Behaviors.Authorization;

namespace WashingMachineManagementApi.Application.Common.Authorization;

public class MustBeInRolesRequirement : IAuthorizationRequirement
{
    public required ICollection<string> UserRoles { get; init; } = default!;
    public required ICollection<string> RequiredRoles { get; init; } = default!;
    class MustBeInRolesRequirementHandler : IAuthorizationHandler<MustBeInRolesRequirement>
    {
        private readonly IJsonStringLocalizer _localizer;

        public MustBeInRolesRequirementHandler(IJsonStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<AuthorizationResult> Handle(MustBeInRolesRequirement request, CancellationToken cancellationToken)
        {
            var isUserInRequiredRoles = request.UserRoles.Any(ur => request.RequiredRoles.Contains(ur));

            if (!isUserInRequiredRoles)
            {
                return AuthorizationResult.Fail(
                    String.Format(
                        _localizer["AuthorizationRequirements.MustBeInRolesRequirement"],
                        $"'{String.Join("', '", request.RequiredRoles)}'"));
            }

            return AuthorizationResult.Succeed();
        }
    }
}
