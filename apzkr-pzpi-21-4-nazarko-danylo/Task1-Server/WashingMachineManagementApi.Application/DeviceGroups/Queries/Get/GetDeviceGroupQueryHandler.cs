using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;

namespace WashingMachineManagementApi.Application.DeviceGroups.Queries.Get;

public class GetDeviceGroupsWithPaginationQueryHandler : IRequestHandler<GetDeviceGroupQuery, DeviceGroupDto>
{
    private readonly IMapper _mapper;
    private readonly IDeviceGroupRepository _repository;

    public GetDeviceGroupsWithPaginationQueryHandler(IDeviceGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeviceGroupDto> Handle(GetDeviceGroupQuery request, CancellationToken cancellationToken)
    {
        var entity = _repository.Queryable.FirstOrDefault(e => e.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<DeviceGroupDto>(entity);
    }
}
