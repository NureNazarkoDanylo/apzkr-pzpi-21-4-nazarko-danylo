using WashingMachineManagementIot.Models;

namespace WashingMachineManagementIot.Services;

public class MockStateService : IStateService
{
    private State state;

    public MockStateService()
    {
        state = State.Idle;
    }

    public State GetState()
    {
        return state;
    }

    public void SetState(State state)
    {
        this.state = state;
    }
}
