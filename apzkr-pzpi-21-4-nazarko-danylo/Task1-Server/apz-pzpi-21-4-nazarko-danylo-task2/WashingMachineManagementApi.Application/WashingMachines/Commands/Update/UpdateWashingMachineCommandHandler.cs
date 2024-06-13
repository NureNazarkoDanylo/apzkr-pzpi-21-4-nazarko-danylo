using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Update;

public class UpdateWashingMachineCommandHandler : IRequestHandler<UpdateWashingMachineCommand, WashingMachineDto>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;

    public UpdateWashingMachineCommandHandler(IWashingMachineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WashingMachineDto> Handle(UpdateWashingMachineCommand request, CancellationToken cancellationToken)
    {
        var isEntityPresentInDatabase = _repository.Queryable.Any(e => e.Id == request.Id);

        if (!isEntityPresentInDatabase)
        {
            throw new NotFoundException();
        }

        var updatedEntity = new WashingMachine()
        {
            Id = request.Id,
            Name = request.Name,
            Manufacturer = request.Manufacturer,
            SerialNumber = request.SerialNumber,
            Description = request.Description,
            DeviceGroupId = request.DeviceGroupId
        };

        var databaseEntity = await _repository.UpdateOneAsync(updatedEntity, cancellationToken);

        return _mapper.Map<WashingMachineDto>(databaseEntity);
    }
}
