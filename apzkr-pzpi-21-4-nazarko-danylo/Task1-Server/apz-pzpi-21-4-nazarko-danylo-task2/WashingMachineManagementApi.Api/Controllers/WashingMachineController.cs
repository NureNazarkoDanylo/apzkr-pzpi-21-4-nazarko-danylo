using Microsoft.AspNetCore.Mvc;
using WashingMachineManagementApi.Application.WashingMachines;
using WashingMachineManagementApi.Application.WashingMachines.Commands.Create;
using WashingMachineManagementApi.Application.WashingMachines.Commands.Delete;
using WashingMachineManagementApi.Application.WashingMachines.Commands.Update;
using WashingMachineManagementApi.Application.WashingMachines.Queries.Get;
using WashingMachineManagementApi.Application.WashingMachines.Queries.GetWithPagination;
using WashingMachineManagementApi.Application.WashingMachines.Queries.Discover;
using WashingMachineManagementApi.Application.Common.Models;
using Newtonsoft.Json;
using WashingMachineManagementApi.Application.WashingMachines.Commands.ChangeState;
using WashingMachineManagementApi.Application.WashingMachines.Queries.GetWashingMachineOperationStatus;

namespace WashingMachineManagementApi.Api.Controllers;

[Route("washingMachines")]
public class WashingMachineController : BaseController
{
    [HttpPost]
    public async Task<WashingMachineDto> Create([FromBody] CreateWashingMachineCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpGet]
    public async Task<PaginatedList<WashingMachineDto>> GetPage([FromQuery] GetWashingMachinesWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<WashingMachineDto> Get(string id, CancellationToken cancellationToken)
    {
        var query = new GetWashingMachineQuery() { Id = id };
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPut]
    public async Task<WashingMachineDto> Update([FromBody] UpdateWashingMachineCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task Delete([FromBody] DeleteWashingMachineCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
    }

    [HttpGet("discover")]
    public async Task Discover(CancellationToken cancellationToken)
    {
        Response.Headers.Add("Content-Type", "text/event-stream");
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");

        var result = Mediator.CreateStream<DiscoveredWashingMachineDto>(new DiscoverWashingMachinesQuery(), cancellationToken);

        await foreach (var discoveredWashingMachineDto in result)
        {
            var jsonString = JsonConvert.SerializeObject(discoveredWashingMachineDto);

            var sseString = $"event: discovered\ndata: {jsonString}\n";

            await Response.WriteAsync(sseString, cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
    }

    [HttpPost("changeState")]
    public async Task ChangeState([FromBody] ChangeWashingMachineStateCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
    }

    [HttpGet("{id}/streamStatus")]
    public async Task StreamStatus(string id, CancellationToken cancellationToken)
    {
        Response.Headers.Add("Content-Type", "text/event-stream");
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");

        var result = Mediator.CreateStream<WashingMachineOperationStatus>(new GetWashingMachineOperationStatusQuery() { Id = id }, cancellationToken);

        await foreach (var washingMachineStatus in result)
        {
            var jsonString = JsonConvert.SerializeObject(washingMachineStatus);

            var sseString = $"event: status\ndata: {jsonString}\n";

            await Response.WriteAsync(sseString, cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
    }
}
