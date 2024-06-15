using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.Create;

public class CreateWashingMachineCommandHandler : IRequestHandler<CreateWashingMachineCommand, WashingMachineDto>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;

    public CreateWashingMachineCommandHandler(IWashingMachineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WashingMachineDto> Handle(CreateWashingMachineCommand request, CancellationToken cancellationToken)
    {
        var newEntity = new WashingMachine()
        {
            Id = request.Id,
            Name = request.Name,
            Manufacturer = request.Manufacturer,
            SerialNumber = request.SerialNumber,
            Description = request.Description,
            DeviceGroupId = request.DeviceGroupId
        };

        var databaseEntity = await _repository.AddOneAsync(newEntity, cancellationToken);

        return _mapper.Map<WashingMachineDto>(databaseEntity);
    }
}
