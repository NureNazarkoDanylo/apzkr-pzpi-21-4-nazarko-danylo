using MediatR;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Update;

public record UpdateDeviceGroupCommand : IRequest<DeviceGroupDto>
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
