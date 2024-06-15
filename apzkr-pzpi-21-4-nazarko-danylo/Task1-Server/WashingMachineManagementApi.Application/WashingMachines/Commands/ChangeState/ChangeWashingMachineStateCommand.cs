using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.ChangeState;

public record ChangeWashingMachineStateCommand : IRequest
{
    public string Id { get; set; }

    public WashingMachineState State { get; set; }
}
