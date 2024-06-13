using FluentValidation;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Update;

public class UpdateDeviceGroupCommandValidator : AbstractValidator<UpdateDeviceGroupCommand>
{
    public UpdateDeviceGroupCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();

        RuleFor(e => e.Name)
            .Length(0, 32);

        RuleFor(e => e.Description)
            .Length(0, 128);
    }
}
