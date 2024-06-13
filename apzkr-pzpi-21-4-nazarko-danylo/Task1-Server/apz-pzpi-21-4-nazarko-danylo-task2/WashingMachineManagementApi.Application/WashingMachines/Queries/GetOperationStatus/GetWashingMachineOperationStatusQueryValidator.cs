using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWashingMachineOperationStatus;

public class CreateWashingMachineCommandValidator : AbstractValidator<GetWashingMachineOperationStatusQuery>
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
    }
}
