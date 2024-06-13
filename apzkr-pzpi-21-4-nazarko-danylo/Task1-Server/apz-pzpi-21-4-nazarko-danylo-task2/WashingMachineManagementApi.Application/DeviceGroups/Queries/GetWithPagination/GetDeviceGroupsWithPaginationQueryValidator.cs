using FluentValidation;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.GetWithPagination;

public class GetDeviceGroupsWithPaginationQueryValidator : AbstractValidator<GetDeviceGroupsWithPaginationQuery>
{
    public GetDeviceGroupsWithPaginationQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(v => v.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
    }
}
