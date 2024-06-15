using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace WashingMachineManagementApi.Application.Common.Services;

public class MqttClientService
{
    private readonly IMqttClient _mqttClient;
    private readonly MqttClientOptions _mqttClientOptions;
    private readonly ILogger<MqttClientService> _logger;

    public MqttClientService(MqttClientOptions options, ILogger<MqttClientService> logger)
    {
        _mqttClientOptions = options;
        _logger = logger;

        _mqttClient = new MqttFactory().CreateMqttClient();

        _mqttClient.DisconnectedAsync += HandleClientDisconnectedAsync;
    }

    public void SubscribeToTopics(IEnumerable<string> topics)
    {
        _mqttClient.ConnectedAsync += async eventArgs =>
        {
            _logger.LogInformation(
                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Connected to MQTT Broker.",
                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                Activity.Current?.TraceId.ToString(),
                Activity.Current?.SpanId.ToString());

            foreach (var topic in topics)
            {
                await _mqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtMostOnce);
            }
        };
    }

    public async Task PublishToTopicAsync(string topic, string payload, CancellationToken cancellationToken)
    {
        // TODO: Handle result
        var result = await _mqttClient.PublishStringAsync(topic, payload, MqttQualityOfServiceLevel.ExactlyOnce, false, cancellationToken);
    }

    private Func<MqttApplicationMessageReceivedEventArgs, Task> HandleApplicationMessageReceivedAsync = _ => Task.CompletedTask;

    public void SetHandleApplicationMessageReceivedAsync(Func<MqttApplicationMessageReceivedEventArgs, Task> del)
    {
        foreach (Func<MqttApplicationMessageReceivedEventArgs, Task> d in HandleApplicationMessageReceivedAsync.GetInvocationList())
        {
            HandleApplicationMessageReceivedAsync -= d;
            _mqttClient.ApplicationMessageReceivedAsync -= d;
        }

        HandleApplicationMessageReceivedAsync = del;
        _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
    }

    private async Task HandleClientDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
    {
        if (eventArgs.Reason == MqttClientDisconnectReason.NormalDisconnection)
        {
            _logger.LogWarning(
                    "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Disconnected from MQTT Broker.",
                    DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                    Activity.Current?.TraceId.ToString(),
                    Activity.Current?.SpanId.ToString());

            return;
        }

        _logger.LogWarning(
                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Lost connection with MQTT Broker. Reason: {@Reason}. Trying to reconnect.",
                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                Activity.Current?.TraceId.ToString(),
                Activity.Current?.SpanId.ToString(),
                eventArgs.Reason);

        _ = Task.Run(
            async () =>
            {
                while (true)
                {
                    try
                    {
                        if (!await _mqttClient.TryPingAsync())
                        {
                            await _mqttClient.ConnectAsync(_mqttClient.Options);
                            _logger.LogWarning(
                                    "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Reconnected to MQTT Broker.",
                                    DateTime.UtcNow.ToString("yyyy-MM-dd"),
                                    DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                                    Activity.Current?.TraceId.ToString(),
                                    Activity.Current?.SpanId.ToString());
                        }
                    }
                    catch (Exception exeption)
                    {
                        _logger.LogError(
                                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Failed connecting to MQTT Broker. {@Exception}",
                                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                                Activity.Current?.TraceId.ToString(),
                                Activity.Current?.SpanId.ToString(),
                                exeption);
                    }
                    finally
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            });

        await Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.ConnectAsync(_mqttClientOptions);

        _ = Task.Run(
            async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                        if (!await _mqttClient.TryPingAsync())
                        {
                            await _mqttClient.ConnectAsync(_mqttClient.Options, cancellationToken);

                            // Subscribe to topics when session is clean etc.
                            Console.WriteLine("The MQTT client is connected.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception properly (logging etc.).
                        Console.WriteLine("The MQTT client  connection failed" + ex);
                    }
                    finally
                    {
                        // Check the connection state every 5 seconds and perform a reconnect if required.
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            var disconnectOption = new MqttClientDisconnectOptions
            {
                Reason = MqttClientDisconnectOptionsReason.NormalDisconnection
            };

            await _mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
        }

        await _mqttClient.DisconnectAsync();
    }
}
