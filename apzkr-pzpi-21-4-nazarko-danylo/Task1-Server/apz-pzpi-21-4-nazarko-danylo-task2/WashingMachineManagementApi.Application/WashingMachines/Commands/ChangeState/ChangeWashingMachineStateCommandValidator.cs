using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.ChangeState;

public class ChangeWashingMachineStateCommandValidator : AbstractValidator<ChangeWashingMachineStateCommand>
{
    private readonly IJsonStringLocalizer _localizer;

    public ChangeWashingMachineStateCommandValidator(IJsonStringLocalizer localizer)
    {
        _localizer = localizer;

        RuleFor(e => e.Id)
            .NotEmpty()
            .WithMessage(e => 
                String.Format(
                    _localizer["RequestValidation.Generic.NotNull"],
                    nameof(e.Id).FirstCharacterToLower()));

        RuleFor(e => e.State)
            .NotEmpty()
            .WithMessage(e => 
                String.Format(
                    _localizer["RequestValidation.Generic.NotNull"],
                    nameof(e.State).FirstCharacterToLower()));
    }
}
