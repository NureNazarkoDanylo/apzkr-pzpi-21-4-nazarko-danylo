using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WashingMachineManagementApi.Application.Common.Services;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.GetWashingMachineOperationStatus;

public class GetWashingMachineOperationStatusQueryHandler : IStreamRequestHandler<GetWashingMachineOperationStatusQuery, WashingMachineOperationStatus>
{
    private readonly IMapper _mapper;
    private readonly MqttClientService _mqttClientService;

    private Queue<WashingMachineOperationStatus> operationStatusResponseQueue = new(4);

    public GetWashingMachineOperationStatusQueryHandler(IMapper mapper, MqttClientService mqttClientService)
    {
        _mapper = mapper;
        _mqttClientService = mqttClientService;
    }

    public async IAsyncEnumerable<WashingMachineOperationStatus> Handle(
        GetWashingMachineOperationStatusQuery request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        _mqttClientService.SubscribeToTopics(new string[] { $"devices/{request.Id}/getStatus" });

        _mqttClientService.SetHandleApplicationMessageReceivedAsync(async eventArgs =>
        {
            var payload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);

            if (String.IsNullOrWhiteSpace(payload))
            {
                return;
            }

            try
            {
                var operationStatusResponse = JsonConvert.DeserializeObject<WashingMachineOperationStatus>(payload);
                operationStatusResponseQueue.Enqueue(operationStatusResponse);
            }
            catch (System.Exception e)
            {
                // TODO: Proper error handling
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
            }
        });

        await _mqttClientService.StartAsync(cancellationToken);

        await _mqttClientService.PublishToTopicAsync($"devices/{request.Id}/getStatus", String.Empty, cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            if (operationStatusResponseQueue.Any())
            {
                var operationStatusResponse = operationStatusResponseQueue.Dequeue();

                yield return operationStatusResponse;

                await Task.Delay(TimeSpan.FromSeconds(2));

                await _mqttClientService.PublishToTopicAsync($"devices/{request.Id}/getStatus", String.Empty, cancellationToken);
            }
        }

        await _mqttClientService.StopAsync(new CancellationToken());
        yield break;
    }
}
