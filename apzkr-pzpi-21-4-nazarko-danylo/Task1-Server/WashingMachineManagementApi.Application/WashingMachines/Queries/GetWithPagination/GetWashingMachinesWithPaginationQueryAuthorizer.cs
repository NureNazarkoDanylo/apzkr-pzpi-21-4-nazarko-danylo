using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWithPagination;

public class GetWashingMachinesWithPaginationQueryAuthorizer : AbstractRequestAuthorizer<GetWashingMachinesWithPaginationQuery>
{
    private readonly ISessionUserService _sessionUserService;

    public GetWashingMachinesWithPaginationQueryAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(GetWashingMachinesWithPaginationQuery request)
    {
        UseRequirement(new MustBeAuthenticatedRequirement
        {
            IsAuthenticated = _sessionUserService.IsAuthenticated
        });
    }
}
