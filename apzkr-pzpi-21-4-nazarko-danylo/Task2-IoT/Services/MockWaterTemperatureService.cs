namespace WashingMachineManagementIot.Services;

public class MockWaterTemperatureService : IWaterTemperatureService
{
    private int WaterTemperatureInCelcius;

    public MockWaterTemperatureService()
    {
        SetRandomWaterTemperatureInCelcius();
    }

    public int GetWaterTemparatureInCelcius()
    {
        return WaterTemperatureInCelcius;
    }

    public int GetWaterTemparatureInFahrenheit()
    {
        throw new NotImplementedException();
    }

    public int GetWaterTemparatureInKelvins()
    {
        throw new NotImplementedException();
    }

    private void SetRandomWaterTemperatureInCelcius()
    {
        var random = new Random();
        WaterTemperatureInCelcius = random.Next(10, 80);
    }
}
