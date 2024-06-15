using WashingMachineManagementIot.Models;

namespace WashingMachineManagementIot.Services;

public interface IStateService
{
    public State GetState();

    public void SetState(State state);
}
