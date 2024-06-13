using WashingMachineManagementApi.Application.Common.Mappings;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Application.DeviceGroups;

public class DeviceGroupDto : IMapFrom<DeviceGroup>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
