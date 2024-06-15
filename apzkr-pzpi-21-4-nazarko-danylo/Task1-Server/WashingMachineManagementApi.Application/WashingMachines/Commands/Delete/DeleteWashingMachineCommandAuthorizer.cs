using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Delete;

public class DeleteWashingMachineCommandAuthorizer : AbstractRequestAuthorizer<DeleteWashingMachineCommand>
{
    private readonly ISessionUserService _sessionUserService;

    public DeleteWashingMachineCommandAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(DeleteWashingMachineCommand request)
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
