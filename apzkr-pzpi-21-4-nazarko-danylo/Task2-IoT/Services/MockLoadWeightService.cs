namespace WashingMachineManagementIot.Services;

public class MockLoadWeightService : ILoadWeightService
{
    public double GetLoadWeightInKg()
    {
        var random = new Random();
        return Math.Round(random.NextDouble() * 15, 2);
    }
}
