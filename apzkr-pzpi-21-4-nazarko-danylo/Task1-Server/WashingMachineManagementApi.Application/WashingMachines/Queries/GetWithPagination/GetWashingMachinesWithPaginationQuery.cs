using WashingMachineManagementApi.Application.Common.Models;
using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWithPagination;

public record GetWashingMachinesWithPaginationQuery : IRequest<PaginatedList<WashingMachineDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? DeviceGroupId { get; set; }
}
