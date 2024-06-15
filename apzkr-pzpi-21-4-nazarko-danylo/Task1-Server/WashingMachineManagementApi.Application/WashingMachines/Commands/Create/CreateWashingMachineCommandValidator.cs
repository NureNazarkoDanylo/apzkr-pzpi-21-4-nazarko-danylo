using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Create;

public class CreateWashingMachineCommandValidator : AbstractValidator<CreateWashingMachineCommand>
{
    private readonly IJsonStringLocalizer _localizer;

    public CreateWashingMachineCommandValidator(IJsonStringLocalizer localizer)
    {
        _localizer = localizer;

        RuleFor(e => e.Id)
            .NotEmpty()
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.NotEmpty"],
                    nameof(e.Id).FirstCharacterToLower()));

        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.NotEmpty"],
                    nameof(e.Name).FirstCharacterToLower()))
            .Length(0, 32)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.Length"],
                    nameof(e.Name).FirstCharacterToLower(), 0, 32));

        RuleFor(e => e.Manufacturer)
            .NotEmpty()
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.NotEmpty"],
                    nameof(e.Manufacturer).FirstCharacterToLower()))
            .Length(0, 64)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.Length"],
                    nameof(e.Name).FirstCharacterToLower(), 0, 64));

        RuleFor(e => e.SerialNumber)
            .NotEmpty()
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.NotEmpty"],
                    nameof(e.SerialNumber).FirstCharacterToLower()))
            .Length(0, 32)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.Length"],
                    nameof(e.SerialNumber).FirstCharacterToLower(), 0, 32));

        RuleFor(e => e.Description)
            .Length(0, 128)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.Length"],
                    nameof(e.Description).FirstCharacterToLower(), 0, 128));
    }
}
