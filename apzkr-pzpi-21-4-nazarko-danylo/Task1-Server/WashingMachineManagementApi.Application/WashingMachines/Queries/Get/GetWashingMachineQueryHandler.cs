using AutoMapper;
using MediatR;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IRepositories;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Get;

public class GetWashingMachinesWithPaginationQueryHandler : IRequestHandler<GetWashingMachineQuery, WashingMachineDto>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;

    public GetWashingMachinesWithPaginationQueryHandler(IWashingMachineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WashingMachineDto> Handle(GetWashingMachineQuery request, CancellationToken cancellationToken)
    {
        var entity = _repository.Queryable.FirstOrDefault(e => e.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<WashingMachineDto>(entity);
    }
}
