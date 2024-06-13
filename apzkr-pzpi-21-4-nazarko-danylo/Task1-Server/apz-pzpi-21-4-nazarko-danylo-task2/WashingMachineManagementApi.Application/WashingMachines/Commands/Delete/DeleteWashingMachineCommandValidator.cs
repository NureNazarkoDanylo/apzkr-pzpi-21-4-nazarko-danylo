using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Delete;

public class DeleteWashingMachineCommandValidator : AbstractValidator<DeleteWashingMachineCommand>
{
    private readonly IJsonStringLocalizer _localizer;

    public DeleteWashingMachineCommandValidator(IJsonStringLocalizer localizer)
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.NotEmpty"],
                    nameof(e.Id).FirstCharacterToLower()));
        _localizer = localizer;
    }
}
