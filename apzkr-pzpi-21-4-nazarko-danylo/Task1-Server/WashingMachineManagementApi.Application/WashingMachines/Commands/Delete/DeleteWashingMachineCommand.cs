using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Delete;

public record DeleteWashingMachineCommand : IRequest
{
    public string Id { get; set; } = null!;
}
