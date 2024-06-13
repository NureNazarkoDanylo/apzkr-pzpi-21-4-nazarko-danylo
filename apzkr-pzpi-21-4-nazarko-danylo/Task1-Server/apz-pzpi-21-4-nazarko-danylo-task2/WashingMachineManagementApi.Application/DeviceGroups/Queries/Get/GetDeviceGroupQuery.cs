using MediatR;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.Get;

public record GetDeviceGroupQuery : IRequest<DeviceGroupDto>
{
    public string Id { get; set; }
}
