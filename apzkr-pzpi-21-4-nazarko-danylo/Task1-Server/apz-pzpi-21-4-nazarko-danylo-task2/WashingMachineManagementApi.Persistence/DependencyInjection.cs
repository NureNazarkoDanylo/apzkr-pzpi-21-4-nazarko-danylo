using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using MongoDB.Bson.Serialization;
using WashingMachineManagementApi.Application.Common.IRepositories;
// using WashingMachineManagementApi.Domain.Entities;
using WashingMachineManagementApi.Persistence.MongoDb;
using WashingMachineManagementApi.Persistence.PostgreSQL;
// using WashingMachineManagementApi.Persistence.MongoDb.Repositories;
// using WashingMachineManagementApi.Persistence.Serializers;
using WashingMachineManagementApi.Persistence.PostgreSQL.Repositories;

namespace WashingMachineManagementApi.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseName = configuration["Persistence:Database:Type"];

        if (databaseName.Equals("mongodb"))
        {
            services.AddMongoDb();
            return services;
        }

        if (databaseName.Equals("postgresql"))
        {
            services.AddPostgreSQL(configuration);
            return services;
        }

        // TODO: Refactor to use database type enum

        throw new ArgumentException(
            $"Unsupported database type specified in configuration. " +
            $"Supported types: mongodb postgresql");
    }

    private static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        // BsonSerializer.RegisterSerializer<Expense>(new ExpenseSerializer());
        // BsonSerializer.RegisterSerializer<Budget>(new BudgetSerializer());

        services.AddSingleton<MongoDbContext>();

        // services.AddScoped<IExpenseRepository, ExpenseMongoDbRepository>();

        return services;
    }

    private static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration["Persistence:Database:ConnectionString"]);
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging(true);
            options.LogTo((message) => Console.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Trace);
        });

        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IWashingMachineRepository, WashingMachinePostgreSQLRepository>();
        services.AddScoped<IDeviceGroupRepository, DeviceGroupPostgreSQLRepository>();
        // services.AddScoped<IDeviceGroupRepository, DevicePostgreSQLRepository>();

        return services;
    }
}
