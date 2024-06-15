using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WashingMachineManagementIot.Models.Responses;
using WashingMachineManagementIot.Models;
using WashingMachineManagementIot.Services;

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

IWaterTemperatureService waterTemperatureService = new MockWaterTemperatureService();
IMotorSpeedService motorSpeedService = new MockMotorSpeedService();
ILoadWeightService loadWeightService = new MockLoadWeightService();
ILidClosedService lidClosedService = new MockLidClosedService();
IStateService stateService = new MockStateService();

var MqttClient = new MqttFactory()
    .CreateMqttClient();

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var configuration = InitializeConfiguration();

AssingHandlers();
await StartMqttClientAsync(cancellationToken);
WaitForStopSignal(cancellationToken);
await StopMqttClientAsync(new CancellationTokenSource().Token);

Configuration InitializeConfiguration()
{
    var configuration = GetDefaultConfiguration();

    if (TryReadConfiguration(out var savedConfiguration))
    {
        configuration = savedConfiguration;
        return configuration;
    }

    WriteConfiguration(configuration);
    return configuration;

    bool TryReadConfiguration(out Configuration configuration)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var pathToFile = Path.Join(currentDirectory, "configuration.json");

        if (File.Exists(pathToFile))
        {
            var json = File.ReadAllText(pathToFile, Encoding.UTF8);
            configuration = JsonConvert.DeserializeObject<Configuration>(json);
            return true;
        }

        configuration = null;
        return false;
    }

    Configuration GetDefaultConfiguration()
    {
        return new Configuration();
    }

    void WriteConfiguration(Configuration configuration)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var pathToFile = Path.Join(currentDirectory, "configuration.json");

        var json = JsonConvert.SerializeObject(configuration);
        File.WriteAllText(pathToFile, json, Encoding.UTF8);
    }
}

void AssingHandlers()
{
    MqttClient.ConnectedAsync += HandleClientConnectedAsync;
    MqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
    MqttClient.DisconnectedAsync += HandleClientDisconnectedAsync;

    async Task HandleClientConnectedAsync(MqttClientConnectedEventArgs eventArgs)
    {
        await MqttClient.SubscribeAsync("devices/discovery", MqttQualityOfServiceLevel.AtMostOnce);
        await MqttClient.SubscribeAsync($"devices/{configuration.Id}/+", MqttQualityOfServiceLevel.AtMostOnce);

        Console.WriteLine("Connected to MQTT Broker.");
    }

    async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        var messageTopic = eventArgs.ApplicationMessage.Topic;
        var messagePayload = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);

        if (messageTopic.Equals("devices/discovery") && messagePayload.Equals("start"))
        {
            await HandleDeviceDiscovery();
        }

        if (messageTopic.Equals($"devices/{configuration.Id}/changeState"))
        {
            Enum.TryParse(messagePayload, out State state);
            await HandleChangeState(state);
        }

        if (messageTopic.Equals($"devices/{configuration.Id}/getStatus"))
        {
            if (!String.IsNullOrWhiteSpace(messagePayload))
            {
                return;
            }

            await HandleGetStatus();
        }

        async Task HandleDeviceDiscovery()
        {
            var response = new DiscoveryResponse()
            {
                Id = configuration.Id,
                Manufacturer = configuration.Manufacturer,
                SerialNumber = configuration.SerialNumber,
                Name = "Generic Washing Machine"
            };

            var payload = JsonConvert.SerializeObject(response);

            await MqttClient.PublishStringAsync(
                $"devices/discovery",
                payload,
                MqttQualityOfServiceLevel.ExactlyOnce,
                false);
        }

        async Task HandleChangeState(WashingMachineManagementIot.Models.State state)
        {
            stateService.SetState(state);
        }

        async Task HandleGetStatus()
        {
            var random = new Random();

            var response = new StatusResponse()
            {
                State = stateService.GetState(),
                WaterTemperatureCelcius = waterTemperatureService.GetWaterTemparatureInCelcius(),
                MotorSpeedRpm = motorSpeedService.GetMotorSpeedInRpm(),
                LoadWeightKg = loadWeightService.GetLoadWeightInKg(),
                IsLidClosed = lidClosedService.GetIsLidCloed()
            };

            var payload = JsonConvert.SerializeObject(response);

            await MqttClient.PublishStringAsync(
                $"devices/{configuration.Id}/getStatus",
                payload,
                MqttQualityOfServiceLevel.ExactlyOnce,
                false,
                new CancellationTokenSource().Token);
        }
    }

    async Task HandleClientDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Lost connection with MQTT Broker. Reason: {eventArgs.Reason}. Trying to reconnect.");

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
                            Console.WriteLine("Reconnected to MQTT Broker.");
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"Failed connecting to MQTT Broker.\n{exception.StackTrace}");
                    }
                    finally
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            });

        await Task.CompletedTask;
    }
}

void WaitForStopSignal(CancellationToken cancellationToken)
{
    Console.CancelKeyPress += async (object? sender, ConsoleCancelEventArgs e) => await StopMqttClientAsync(cancellationToken);

    while (true)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            continue;
        }

        return;
    }
}

async Task StartMqttClientAsync(CancellationToken cancellationToken)
{
    var MqttClientOptions = new MqttClientOptionsBuilder()
        .WithCredentials("", "")
        .WithClientId("")
        .WithTcpServer(configuration.Host, configuration.Port)
        .Build();

    await MqttClient.ConnectAsync(MqttClientOptions);

    _ = Task.Run(
        async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (!await MqttClient.TryPingAsync())
                    {
                        await MqttClient.ConnectAsync(MqttClient.Options, cancellationToken);
                        Console.WriteLine("The MQTT client is connected.");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"The MQTT client connection failed.\n{exception.StackTrace}");
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        });
}

async Task StopMqttClientAsync(CancellationToken cancellationToken)
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

    Console.WriteLine("Disconnected from MQTT Broker.");
}
