using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.Get;

public class GetDeviceGroupQueryAuthorizer : AbstractRequestAuthorizer<GetDeviceGroupQuery>
{
    private readonly ISessionUserService _sessionUserService;
    private readonly IDeviceGroupRepository _repository;

    public GetDeviceGroupQueryAuthorizer(ISessionUserService currentUserService, IDeviceGroupRepository repository)
    {
        _sessionUserService = currentUserService;
        _repository = repository;
    }

    public override void BuildPolicy(GetDeviceGroupQuery request)
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
