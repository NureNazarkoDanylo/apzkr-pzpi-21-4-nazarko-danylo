using MediatR;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Create;

public record CreateDeviceGroupCommand : IRequest<DeviceGroupDto>
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
