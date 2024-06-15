using FluentValidation;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Discover;

public class CreateWashingMachineCommandValidator : AbstractValidator<DiscoverWashingMachinesQuery>
{
    public CreateWashingMachineCommandValidator()
    {
    }
}
