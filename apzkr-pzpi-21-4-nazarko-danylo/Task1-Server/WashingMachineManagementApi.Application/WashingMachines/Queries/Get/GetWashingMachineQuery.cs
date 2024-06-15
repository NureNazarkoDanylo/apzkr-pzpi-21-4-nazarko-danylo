using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Get;

public record GetWashingMachineQuery : IRequest<WashingMachineDto>
{
    public string Id { get; set; }
}
