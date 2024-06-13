namespace WashingMachineManagementApi.Application.WashingMachines;

public class WashingMachineOperationStatus
{
    public string State { get; set; }

    public int WaterTemperatureCelcius { get; set; }

    public int MotorSpeedRPM { get; set; }
    
    public bool IsLidClosed { get; set; }

    public double LoadWeightKg { get; set; }
}
