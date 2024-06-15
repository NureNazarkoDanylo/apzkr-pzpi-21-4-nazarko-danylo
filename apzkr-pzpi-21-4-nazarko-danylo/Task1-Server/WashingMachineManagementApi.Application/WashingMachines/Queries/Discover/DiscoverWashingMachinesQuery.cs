using MediatR;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Discover;

public record DiscoverWashingMachinesQuery : IStreamRequest<DiscoveredWashingMachineDto> { }
