using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Create;

public class CreateWashingMachineCommandAuthorizer : AbstractRequestAuthorizer<CreateWashingMachineCommand>
{
    private readonly ISessionUserService _sessionUserService;

    public CreateWashingMachineCommandAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(CreateWashingMachineCommand request)
    {
        UseRequirement(new MustBeAuthenticatedRequirement
        {
            IsAuthenticated = _sessionUserService.IsAuthenticated
        });

        UseRequirement(new MustBeInRolesRequirement
        {
            UserRoles = _sessionUserService.Roles,
            RequiredRoles = new string[] { IdentityRoles.Administrator.ToString() }
        });
    }
}
