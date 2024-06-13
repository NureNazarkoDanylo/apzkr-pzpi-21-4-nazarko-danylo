using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Update;

public class UpdateDeviceGroupCommandAuthorizer : AbstractRequestAuthorizer<UpdateDeviceGroupCommand>
{
    private readonly ISessionUserService _sessionUserService;

    public UpdateDeviceGroupCommandAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(UpdateDeviceGroupCommand request)
    {
        // UseRequirement(new MustBeAuthenticatedRequirement
        // {
        //     IsAuthenticated = _sessionUserService.IsAuthenticated
        // });
        //
        // UseRequirement(new MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement
        // {
        //     UserId = request.UserId,
        //     UserRoles = _sessionUserService.Roles,
        //     RequiredUserId = _sessionUserService.Id,
        //     RequiredRoles = new[] { IdentityRoles.Administrator.ToString() }
        // });
    }
}
