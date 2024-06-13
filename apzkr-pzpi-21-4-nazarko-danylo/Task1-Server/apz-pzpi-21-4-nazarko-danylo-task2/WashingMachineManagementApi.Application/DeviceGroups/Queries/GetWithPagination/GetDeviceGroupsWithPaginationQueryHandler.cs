using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Extensions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.GetWithPagination;

public class GetDeviceGroupsWithPaginationQueryHandler : IRequestHandler<GetDeviceGroupsWithPaginationQuery, PaginatedList<DeviceGroupDto>>
{
    private readonly IMapper _mapper;
    private readonly IDeviceGroupRepository _repository;
    private readonly ISessionUserService _sessionUserService;

    public GetDeviceGroupsWithPaginationQueryHandler(IDeviceGroupRepository repository, IMapper mapper, ISessionUserService sessionUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionUserService = sessionUserService;
    }

    public async Task<PaginatedList<DeviceGroupDto>> Handle(GetDeviceGroupsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.Queryable;

        return entities
            .ProjectToPaginatedList<DeviceGroup, DeviceGroupDto>(request.PageNumber, request.PageSize, _mapper.ConfigurationProvider);
    }
}
