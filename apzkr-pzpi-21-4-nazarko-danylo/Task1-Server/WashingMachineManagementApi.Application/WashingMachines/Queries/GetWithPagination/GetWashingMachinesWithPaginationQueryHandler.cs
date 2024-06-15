using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Extensions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWithPagination;

public class GetWashingMachinesWithPaginationQueryHandler : IRequestHandler<GetWashingMachinesWithPaginationQuery, PaginatedList<WashingMachineDto>>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;
    private readonly ISessionUserService _sessionUserService;

    public GetWashingMachinesWithPaginationQueryHandler(IWashingMachineRepository repository, IMapper mapper, ISessionUserService sessionUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionUserService = sessionUserService;
    }

    public async Task<PaginatedList<WashingMachineDto>> Handle(GetWashingMachinesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.Queryable;

        if (!String.IsNullOrWhiteSpace(request.DeviceGroupId))
        {
            entities = entities.Where(e => e.DeviceGroupId.Equals(request.DeviceGroupId));
        }

        return entities
            .ProjectToPaginatedList<WashingMachine, WashingMachineDto>(request.PageNumber, request.PageSize, _mapper.ConfigurationProvider);
    }
}
