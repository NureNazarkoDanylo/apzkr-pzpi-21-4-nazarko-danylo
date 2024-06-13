using WashingMachineManagementApi.Application.Common.Models;
using MediatR;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.GetWithPagination;

public record GetDeviceGroupsWithPaginationQuery : IRequest<PaginatedList<DeviceGroupDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
