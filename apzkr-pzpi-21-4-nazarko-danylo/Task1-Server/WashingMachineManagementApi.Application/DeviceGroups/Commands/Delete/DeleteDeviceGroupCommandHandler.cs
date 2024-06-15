using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.DeviceGroups.Commands.Delete;

namespace WashingMachineManagementApi.Application.DeviceGroupes.Commands.Delete;

public class DeleteDeviceGroupCommandHandler : IRequestHandler<DeleteDeviceGroupCommand>
{
    private readonly IMapper _mapper;
    private readonly IDeviceGroupRepository _repository;

    public DeleteDeviceGroupCommandHandler(IDeviceGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteDeviceGroupCommand request, CancellationToken cancellationToken)
    {
        var isEntityPresentInDatabase = _repository.Queryable.Any(e => e.Id == request.Id);

        if (!isEntityPresentInDatabase)
        {
            throw new NotFoundException();
        }

        await _repository.DeleteOneAsync(request.Id, cancellationToken);
    }
}
