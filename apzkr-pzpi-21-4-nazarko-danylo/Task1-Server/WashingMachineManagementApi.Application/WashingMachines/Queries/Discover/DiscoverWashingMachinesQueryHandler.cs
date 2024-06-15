using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WashingMachineManagementApi.Application.Common.Services;

namespace WashingMachineManagementApi.Application.WashingMachines.Queries.Discover;

public class DiscoverWashingMachinesQueryHandler : IStreamRequestHandler<DiscoverWashingMachinesQuery, DiscoveredWashingMachineDto>
{
    private readonly IMapper _mapper;
    private readonly MqttClientService _mqttClientService;

    private Queue<DiscoveredWashingMachineDto> discoveryResponseQueue = new(4);

    public DiscoverWashingMachinesQueryHandler(IMapper mapper, MqttClientService mqttClientService)
    {
        _mapper = mapper;
        _mqttClientService = mqttClientService;

        _mqttClientService.SubscribeToTopics(new string[] { "devices/discovery" });

        _mqttClientService.SetHandleApplicationMessageReceivedAsync(async eventArgs =>
        {
            var payload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);

            if (payload.Equals("start"))
            {
                return;
            }

            try
            {
                var discoveryResponse = JsonConvert.DeserializeObject<DiscoveredWashingMachineDto>(payload);
                discoveryResponseQueue.Enqueue(discoveryResponse);
            }
            catch (System.Exception e)
            {
                // TODO: Proper error handling
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
            }
        });
    }

    public async IAsyncEnumerable<DiscoveredWashingMachineDto> Handle(
        DiscoverWashingMachinesQuery request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await _mqttClientService.StartAsync(cancellationToken);

        // Payload can be anything. In this case we care only about topic
        await _mqttClientService.PublishToTopicAsync("devices/discovery", "start", cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            if (discoveryResponseQueue.Any())
            {
                var discoveryResponse = discoveryResponseQueue.Dequeue();
                yield return discoveryResponse;
            }
        }

        await _mqttClientService.StopAsync(new CancellationToken());
        yield break;
    }
}
