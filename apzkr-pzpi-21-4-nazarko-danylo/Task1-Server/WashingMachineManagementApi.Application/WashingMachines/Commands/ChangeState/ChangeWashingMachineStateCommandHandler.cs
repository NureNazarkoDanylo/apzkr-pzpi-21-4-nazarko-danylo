using System.Text;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Application.Common.Services;
using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Domain.Enums;

namespace WashingMachineManagementApi.Application.WashingMachines.Commands.ChangeState;

public class ChangeWashingMachineStateCommandHandler : IRequestHandler<ChangeWashingMachineStateCommand>
{
    private readonly IMapper _mapper;
    private readonly IWashingMachineRepository _repository;
    private readonly MqttClientService _mqttClientService;

    public ChangeWashingMachineStateCommandHandler(IWashingMachineRepository repository, IMapper mapper, MqttClientService mqttClientService)
    {
        _repository = repository;
        _mapper = mapper;
        _mqttClientService = mqttClientService;
    }

    public async Task Handle(ChangeWashingMachineStateCommand request, CancellationToken cancellationToken)
    {
        await _mqttClientService.StartAsync(cancellationToken);
        await _mqttClientService.PublishToTopicAsync($"devices/{request.Id}/changeState", $"{request.State.ToString()}", cancellationToken);
    }
}
