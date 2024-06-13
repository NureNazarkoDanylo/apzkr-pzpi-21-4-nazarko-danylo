using MediatR;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Delete;

public record DeleteDeviceGroupCommand : IRequest
{
    public string Id { get; set; } = null!;
}
