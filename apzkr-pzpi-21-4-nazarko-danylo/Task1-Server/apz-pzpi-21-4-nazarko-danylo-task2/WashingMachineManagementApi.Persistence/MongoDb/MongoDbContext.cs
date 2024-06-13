using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace WashingMachineManagementApi.Persistence.MongoDb;

public class MongoDbContext
{
    public MongoDbContext(IConfiguration configuration, ILogger<MongoDbContext> logger)
    {
        var settings = new MongoClientSettings();

        settings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
                {
                    logger.LogDebug(
                        "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Starting performing MongoDB command {@CommandName}.\nQuery: {@Query}.",
                        DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                        Activity.Current?.TraceId.ToString(),
                        Activity.Current?.SpanId.ToString(),
                        e.CommandName,
                        e.Command.ToString());
                });
            cb.Subscribe<CommandFailedEvent>(e =>
                {
                    logger.LogCritical(
                        "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} MongoDB command {@CommandName} failed with exception {@Exception}.",
                        DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                        Activity.Current?.TraceId.ToString(),
                        Activity.Current?.SpanId.ToString(),
                        e.CommandName,
                        e.Failure);
                });
            cb.Subscribe<CommandSucceededEvent>(e =>
                {
                    logger.LogDebug(
                        "{@DateUtc} {@TimeUtc} {@TraceId} {@SpanId} Finished performing MongoDB command {@CommandName} in {@DurationInSeconds} seconds.",
                        DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        DateTime.UtcNow.ToString("HH:mm:ss.FFF"),
                        Activity.Current?.TraceId.ToString(),
                        Activity.Current?.SpanId.ToString(),
                        e.CommandName,
                        e.Duration.TotalSeconds);
                });
        };

        settings.Server = new MongoServerAddress(configuration["Database:ConnectionString"].Split(':')[0]);

        var client = new MongoClient(settings);
        Db = client.GetDatabase(configuration["Database:DomainPartitionName"]);
    }

    public IMongoDatabase Db { get; private set; }
}
