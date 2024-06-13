using FluentValidation;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Delete;

public class DeleteDeviceGroupCommandValidator : AbstractValidator<DeleteDeviceGroupCommand>
{
    public DeleteDeviceGroupCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();
    }
}
