using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Get;

public class GetWashingMachineQueryValidator : AbstractValidator<GetWashingMachineQuery>
{
    private readonly IJsonStringLocalizer _localizer;

    public GetWashingMachineQueryValidator(IJsonStringLocalizer localizer)
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
