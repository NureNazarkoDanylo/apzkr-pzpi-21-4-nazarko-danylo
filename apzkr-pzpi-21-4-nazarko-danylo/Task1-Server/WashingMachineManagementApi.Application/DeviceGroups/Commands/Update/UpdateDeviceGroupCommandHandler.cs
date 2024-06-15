using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Update;

public class UpdateDeviceGroupCommandHandler : IRequestHandler<UpdateDeviceGroupCommand, DeviceGroupDto>
{
    private readonly IMapper _mapper;
    private readonly IDeviceGroupRepository _repository;

    public UpdateDeviceGroupCommandHandler(IDeviceGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeviceGroupDto> Handle(UpdateDeviceGroupCommand request, CancellationToken cancellationToken)
    {
        var isEntityPresentInDatabase = _repository.Queryable.Any(e => e.Id == request.Id);

        if (!isEntityPresentInDatabase)
        {
            throw new NotFoundException();
        }

        var updatedEntity = new DeviceGroup()
        {
            Name = request.Name,
            Description = request.Description,
        };

        var databaseEntity = await _repository.UpdateOneAsync(updatedEntity, cancellationToken);

        return _mapper.Map<DeviceGroupDto>(databaseEntity);
    }
}
