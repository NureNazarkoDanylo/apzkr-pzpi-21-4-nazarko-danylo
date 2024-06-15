using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Create;

public class CreateDeviceGroupCommandAuthorizer : AbstractRequestAuthorizer<CreateDeviceGroupCommand>
{
    private readonly ISessionUserService _sessionUserService;
    private readonly IDeviceGroupRepository _repository;

    public CreateDeviceGroupCommandAuthorizer(ISessionUserService currentUserService, IDeviceGroupRepository repository)
    {
        _sessionUserService = currentUserService;
        _repository = repository;
    }

    public override void BuildPolicy(CreateDeviceGroupCommand request)
    {
        // UseRequirement(new MustBeAuthenticatedRequirement
        // {
        //     IsAuthenticated = _sessionUserService.IsAuthenticated
        // });
        //
        // UseRequirement(new MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement
        // {
        //     UserId = _sessionUserService.Id,
        //     UserRoles = _sessionUserService.Roles,
        //     RequiredUserId = request.UserId ?? _sessionUserService.Id,
        //     RequiredRoles = new[] { IdentityRoles.Administrator.ToString() }
        // });
    }
}
