namespace WashingMachineManagementIot.Models.Responses;

public class StatusResponse
{
    public State State { get; set; }

    public int WaterTemperatureCelcius { get; set; }

    public int MotorSpeedRpm { get; set; }
    
    public bool IsLidClosed { get; set; }

    public double LoadWeightKg { get; set; }
}
