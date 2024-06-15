using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.WashingMachines.Commands.Delete;

namespace WashingMachineManagementApi.Application.WashingMachinees.Commands.Delete;

public class DeleteWashingMachineCommandHandler : IRequestHandler<DeleteWashingMachineCommand>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;

    public DeleteWashingMachineCommandHandler(IWashingMachineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteWashingMachineCommand request, CancellationToken cancellationToken)
    {
        var isEntityPresentInDatabase = _repository.Queryable.Any(e => e.Id == request.Id);

        if (!isEntityPresentInDatabase)
        {
            throw new NotFoundException();
        }

        await _repository.DeleteOneAsync(request.Id, cancellationToken);
    }
}
