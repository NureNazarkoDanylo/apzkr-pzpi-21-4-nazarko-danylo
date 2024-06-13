using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.DeviceGroups.Commands.Create;

public class CreateDeviceGroupCommandHandler : IRequestHandler<CreateDeviceGroupCommand, DeviceGroupDto>
{
    private readonly IMapper _mapper;
    private readonly IDeviceGroupRepository _repository;

    public CreateDeviceGroupCommandHandler(IDeviceGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeviceGroupDto> Handle(CreateDeviceGroupCommand request, CancellationToken cancellationToken)
    {

        var newEntity = new DeviceGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Description = request.Description,
        };

        var databaseEntity = await _repository.AddOneAsync(newEntity, cancellationToken);

        return _mapper.Map<DeviceGroupDto>(databaseEntity);
    }
}
