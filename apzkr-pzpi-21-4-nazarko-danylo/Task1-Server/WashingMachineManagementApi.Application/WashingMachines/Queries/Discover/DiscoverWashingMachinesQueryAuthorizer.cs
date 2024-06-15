using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Discover;

public class DiscoverWashingMachinesQueryAuthorizer : AbstractRequestAuthorizer<DiscoverWashingMachinesQuery>
{
    private readonly ISessionUserService _sessionUserService;

    public DiscoverWashingMachinesQueryAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(DiscoverWashingMachinesQuery request)
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
