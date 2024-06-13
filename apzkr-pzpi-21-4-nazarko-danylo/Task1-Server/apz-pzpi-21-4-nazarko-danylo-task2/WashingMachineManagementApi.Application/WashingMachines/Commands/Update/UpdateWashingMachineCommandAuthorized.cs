using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Update;

public class UpdateWashingMachineCommandAuthorizer : AbstractRequestAuthorizer<UpdateWashingMachineCommand>
{
    private readonly ISessionUserService _sessionUserService;

    public UpdateWashingMachineCommandAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(UpdateWashingMachineCommand request)
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
