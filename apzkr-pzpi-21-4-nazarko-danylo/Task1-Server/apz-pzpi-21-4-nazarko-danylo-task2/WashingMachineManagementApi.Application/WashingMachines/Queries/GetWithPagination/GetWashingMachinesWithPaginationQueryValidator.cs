using AspNetCore.Localizer.Json.Localizer;
using FluentValidation;
using WashingMachineManagementApi.Application.Common.Extensions;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWithPagination;

public class GetWashingMachinesWithPaginationQueryValidator : AbstractValidator<GetWashingMachinesWithPaginationQuery>
{
    private readonly IJsonStringLocalizer _localizer;

    public GetWashingMachinesWithPaginationQueryValidator(IJsonStringLocalizer localizer)
    {
        _localizer = localizer;

        RuleFor(v => v.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.GreaterThanOrEqualTo"],
                    nameof(e.PageNumber).FirstCharacterToLower(), 1));

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage(e =>
                String.Format(
                    _localizer["RequestValidation.Generic.GreaterThanOrEqualTo"],
                    nameof(e.PageSize).FirstCharacterToLower(), 1))
            .LessThanOrEqualTo(50)
            .WithMessage(e =>
               String.Format(
                    _localizer["RequestValidation.Generic.LessThanOrEqualTo"],
                    nameof(e.PageSize).FirstCharacterToLower(), 50));
    }
}
