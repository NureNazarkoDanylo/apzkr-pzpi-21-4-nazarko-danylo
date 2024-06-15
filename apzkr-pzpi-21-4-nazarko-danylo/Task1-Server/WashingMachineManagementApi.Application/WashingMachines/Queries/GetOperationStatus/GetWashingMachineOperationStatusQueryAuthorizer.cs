using MediatR.Behaviors.Authorization;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.IServices;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWashingMachineOperationStatus;

public class GetWashingMachineOperationStatusQueryAuthorizer : AbstractRequestAuthorizer<GetWashingMachineOperationStatusQuery>
{
    private readonly ISessionUserService _sessionUserService;

    public GetWashingMachineOperationStatusQueryAuthorizer(ISessionUserService currentUserService)
    {
        _sessionUserService = currentUserService;
    }

    public override void BuildPolicy(GetWashingMachineOperationStatusQuery request)
    {
        UseRequirement(new MustBeAuthenticatedRequirement
        {
            IsAuthenticated = _sessionUserService.IsAuthenticated
        });
    }
}
