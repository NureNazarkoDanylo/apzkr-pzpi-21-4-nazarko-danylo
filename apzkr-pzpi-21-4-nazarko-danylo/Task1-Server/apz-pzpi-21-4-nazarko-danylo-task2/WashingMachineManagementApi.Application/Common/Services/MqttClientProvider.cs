using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace WashingMachineManagementApi.Application.Common.Services;

public class MqttClientProvider
{
    public readonly IMqttClient MqttClient;

    private readonly MqttClientOptions _mqttClientOptions;
    private readonly ILogger<MqttClientService> _logger;

    public MqttClientProvider(MqttClientOptions options, ILogger<MqttClientService> logger)
    {
        _mqttClientOptions = options;
        _logger = logger;

        MqttClient = new MqttFactory().CreateMqttClient();

        MqttClient.DisconnectedAsync += HandleClientDisconnectedAsync;
    }

    public void SubscribeToTopics(IEnumerable<string> topics)
    {
        MqttClient.ConnectedAsync += async eventArgs =>
        {
            _logger.LogInformation(
                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Connected to MQTT Broker.",
                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                Activity.Current?.TraceId.ToString(),
                Activity.Current?.SpanId.ToString());

            foreach (var topic in topics)
            {
                await MqttClient.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtMostOnce);
            }
        };
    }

    private async Task HandleClientDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
    {
        if (eventArgs.Reason == MqttClientDisconnectReason.NormalDisconnection)
        {
            _logger.LogInformation(
                    "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Disconnected from MQTT Broker.",
                    DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                    Activity.Current?.TraceId.ToString(),
                    Activity.Current?.SpanId.ToString());

            return;
        }

        _logger.LogWarning(
                "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Lost connection with MQTT Broker. Trying to reconnect.",
                DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                Activity.Current?.TraceId.ToString(),
                Activity.Current?.SpanId.ToString());

        _ = Task.Run(
            async () =>
            {
                while (true)
                {
                    try
                    {
                        if (!await MqttClient.TryPingAsync())
                        {
                            await MqttClient.ConnectAsync(MqttClient.Options);
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
        await MqttClient.ConnectAsync(_mqttClientOptions);

        _ = Task.Run(
            async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                        if (!await MqttClient.TryPingAsync())
                        {
                            await MqttClient.ConnectAsync(MqttClient.Options, cancellationToken);

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

            await MqttClient.DisconnectAsync(disconnectOption, cancellationToken);
        }

        await MqttClient.DisconnectAsync();
    }
}
