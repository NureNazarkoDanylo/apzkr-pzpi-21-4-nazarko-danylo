using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.GetWithPagination;

public class GetDeviceGroupsWithPaginationQueryAuthorizer : AbstractRequestAuthorizer<GetDeviceGroupsWithPaginationQuery>
{
    private readonly ISessionUserService _sessionUserService;

    public GetDeviceGroupsWithPaginationQueryAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(GetDeviceGroupsWithPaginationQuery request)
    {
        // UseRequirement(new MustBeAuthenticatedRequirement
        // {
        //     IsAuthenticated = _sessionUserService.IsAuthenticated
        // });
        //
        // if (request.GetAll)
        // {
        //     UseRequirement(new MustBeInRolesRequirement
        //     {
        //         UserRoles = _sessionUserService.Roles,
        //         RequiredRoles = new[] { IdentityRoles.Administrator.ToString() }
        //     });
        // }
    }
}
