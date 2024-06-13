namespace WashingMachineManagementIot.Services;

public class MockMotorSpeedService : IMotorSpeedService
{
    public int GetMotorSpeedInRpm()
    {
        var random = new Random();
        return random.Next(0, 600);
    }
}
