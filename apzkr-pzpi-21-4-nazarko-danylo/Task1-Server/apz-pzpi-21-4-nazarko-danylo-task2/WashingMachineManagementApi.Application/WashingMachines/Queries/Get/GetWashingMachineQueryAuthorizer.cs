using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Get;

public class GetWashingMachineQueryAuthorizer : AbstractRequestAuthorizer<GetWashingMachineQuery>
{
    private readonly ISessionUserService _sessionUserService;

    public GetWashingMachineQueryAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(GetWashingMachineQuery request)
    {
        UseRequirement(new MustBeAuthenticatedRequirement
        {
            IsAuthenticated = _sessionUserService.IsAuthenticated
        });
    }
}
