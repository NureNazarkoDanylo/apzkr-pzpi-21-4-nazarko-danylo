namespace WashingMachineManagementApi.Domain.Entities;

public class WashingMachine : EntityBase<string>
{
    public string? Name { get; set; }

    public string? Manufacturer { get; set; }

    public string? SerialNumber { get; set; }

    public string? Description { get; set; }

    public DeviceGroup? DeviceGroup { get; set; }

    public string? DeviceGroupId { get; set; }
}
