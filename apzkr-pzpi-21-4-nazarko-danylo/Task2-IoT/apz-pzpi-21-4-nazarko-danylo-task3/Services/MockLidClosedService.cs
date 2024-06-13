namespace WashingMachineManagementIot.Services;

public class MockLidClosedService : ILidClosedService
{
    public bool GetIsLidCloed()
    {
        var random = new Random();
        return random.Next(0, 2) == 1 ? true : false;
    }
}
