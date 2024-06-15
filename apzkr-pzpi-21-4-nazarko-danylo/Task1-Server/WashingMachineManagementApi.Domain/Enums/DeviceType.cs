namespace WashingMachineManagementApi.Domain.Enums;

public abstract class DeviceType : Enumeration<DeviceType>
{
    public static readonly DeviceType WashingMachine = new WashingMachineDeviceType();
    public static readonly DeviceType DryingMachine = new DryingMachineDeviceType();

    protected DeviceType(int value, string name) : base(value, name) { }

    private sealed class WashingMachineDeviceType : DeviceType
    {
        public WashingMachineDeviceType() : base(1, "WashingMachine") { }
    }

    private sealed class DryingMachineDeviceType : DeviceType
    {
        public DryingMachineDeviceType() : base(2, "DryingMachine") { }
    }
}
