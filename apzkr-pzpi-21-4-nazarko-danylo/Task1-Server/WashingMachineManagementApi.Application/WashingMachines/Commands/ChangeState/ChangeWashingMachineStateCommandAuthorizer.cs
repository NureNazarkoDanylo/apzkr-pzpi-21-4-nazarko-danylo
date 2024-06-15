using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.ChangeState;

public class ChangeWashingMachineStateCommandAuthorizer : AbstractRequestAuthorizer<ChangeWashingMachineStateCommand>
{
    private readonly ISessionUserService _sessionUserService;

    public ChangeWashingMachineStateCommandAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(ChangeWashingMachineStateCommand request)
    {
        UseRequirement(new MustBeAuthenticatedRequirement
        {
            IsAuthenticated = _sessionUserService.IsAuthenticated
        });

        UseRequirement(new MustBeInRolesRequirement
        {
            UserRoles = _sessionUserService.Roles,
            RequiredRoles = new string[] { IdentityRoles.User.ToString(), IdentityRoles.Administrator.ToString() }
        });
    }
}
