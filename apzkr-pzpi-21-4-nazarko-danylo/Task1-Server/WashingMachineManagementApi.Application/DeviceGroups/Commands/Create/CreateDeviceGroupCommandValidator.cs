using FluentValidation;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Create;

public class CreateDeviceGroupCommandValidator : AbstractValidator<CreateDeviceGroupCommand>
{
    public CreateDeviceGroupCommandValidator()
    {
        RuleFor(e => e.Name)
            .Length(0, 32);

        RuleFor(e => e.Description)
            .Length(0, 128);
    }
}
