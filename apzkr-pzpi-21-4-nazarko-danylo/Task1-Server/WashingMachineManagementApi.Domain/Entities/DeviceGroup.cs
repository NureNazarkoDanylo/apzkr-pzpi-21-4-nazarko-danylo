namespace WashingMachineManagementApi.Domain.Entities;

public class DeviceGroup : EntityBase<string>
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
