using AutoMapper;
using WashingMachineManagementApi.Application.Common.Mappings;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.WashingMachines;

public class WashingMachineDto : IMapFrom<WashingMachine>
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Manufacturer { get; set; }

    public string? SerialNumber { get; set; }

    public string? Description { get; set; }

    public string? DeviceGroupId { get; set; }
}
