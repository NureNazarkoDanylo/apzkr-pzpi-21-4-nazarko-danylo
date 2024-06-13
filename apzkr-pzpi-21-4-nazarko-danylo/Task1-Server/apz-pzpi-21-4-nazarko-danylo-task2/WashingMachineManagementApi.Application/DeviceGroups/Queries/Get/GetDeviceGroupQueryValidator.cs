using FluentValidation;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.Get;

public class GetDeviceGroupQueryValidator : AbstractValidator<GetDeviceGroupQuery>
{
    public GetDeviceGroupQueryValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}
