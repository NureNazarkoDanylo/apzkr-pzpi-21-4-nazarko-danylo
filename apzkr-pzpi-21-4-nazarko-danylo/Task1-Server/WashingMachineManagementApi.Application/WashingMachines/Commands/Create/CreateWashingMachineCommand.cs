using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Create;

public record CreateWashingMachineCommand : IRequest<WashingMachineDto>
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Manufacturer { get; set; }

    public string? SerialNumber { get; set; }

    public string? Description { get; set; }

    public string? DeviceGroupId { get; set; }
}
