namespace WashingMachineManagementIot.Services;

public interface IWaterTemperatureService
{
    public int GetWaterTemparatureInCelcius();

    public int GetWaterTemparatureInFahrenheit();

    public int GetWaterTemparatureInKelvins();
}
