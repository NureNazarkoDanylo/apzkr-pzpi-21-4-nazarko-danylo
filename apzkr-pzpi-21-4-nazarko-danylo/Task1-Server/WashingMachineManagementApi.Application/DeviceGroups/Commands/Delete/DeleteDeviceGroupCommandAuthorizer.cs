using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Delete;

public class DeleteDeviceGroupCommandAuthorizer : AbstractRequestAuthorizer<DeleteDeviceGroupCommand>
{
    private readonly ISessionUserService _sessionUserService;
    private readonly IDeviceGroupRepository _repository;

    public DeleteDeviceGroupCommandAuthorizer(ISessionUserService currentUserService, IDeviceGroupRepository repository)
    {
        _sessionUserService = currentUserService;
        _repository = repository;
    }

    public override void BuildPolicy(DeleteDeviceGroupCommand request)
    {
        // UseRequirement(new MustBeAuthenticatedRequirement
        // {
        //     IsAuthenticated = _sessionUserService.IsAuthenticated
        // });
        //
        // var requiredUserId = _repository.Queryable.FirstOrDefault(e => e.Id == request.Id)?.UserId;
        //
        // UseRequirement(new MustBeInRolesWhenInteractingWithUnOwnedEntityRequirement
        // {
        //     UserId = _sessionUserService.Id,
        //     UserRoles = _sessionUserService.Roles,
        //     RequiredUserId = requiredUserId,
        //     RequiredRoles = new[] { IdentityRoles.Administrator.ToString() }
        // });
    }
}
