using Microsoft.AspNetCore.Mvc;
using WashingMachineManagementApi.Application.DeviceGroups;
using WashingMachineManagementApi.Application.DeviceGroups.Commands.Create;
using WashingMachineManagementApi.Application.DeviceGroups.Commands.Delete;
using WashingMachineManagementApi.Application.DeviceGroups.Commands.Update;
using WashingMachineManagementApi.Application.DeviceGroups.Queries.Get;
using WashingMachineManagementApi.Application.DeviceGroups.Queries.GetWithPagination;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Api.Controllers;

[Route("deviceGroups")]
public class DeviceGroupController : BaseController
{
    [HttpPost]
    public async Task<DeviceGroupDto> Create([FromBody] CreateDeviceGroupCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpGet]
    public async Task<PaginatedList<DeviceGroupDto>> GetPage([FromQuery] GetDeviceGroupsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<DeviceGroupDto> Get(string id, CancellationToken cancellationToken)
    {
        var query = new GetDeviceGroupQuery() { Id = id };
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPut]
    public async Task<DeviceGroupDto> Update([FromBody] UpdateDeviceGroupCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task Delete([FromBody] DeleteDeviceGroupCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
    }
}
