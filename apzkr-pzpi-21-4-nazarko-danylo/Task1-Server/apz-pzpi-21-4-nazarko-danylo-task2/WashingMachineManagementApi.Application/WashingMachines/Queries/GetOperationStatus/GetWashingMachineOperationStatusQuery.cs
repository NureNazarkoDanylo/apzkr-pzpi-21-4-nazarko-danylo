using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWashingMachineOperationStatus;

public record GetWashingMachineOperationStatusQuery : IStreamRequest<WashingMachineOperationStatus>
{
    public string Id { get; set; }
}
