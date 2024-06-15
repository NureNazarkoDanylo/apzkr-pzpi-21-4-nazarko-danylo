using System.Reflection;
using AspNetCore.Localizer.Json.Extensions;
using AspNetCore.Localizer.Json.JsonOptions;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client;
using WashingMachineManagementApi.Application.Common.Authorization;
using WashingMachineManagementApi.Application.Common.Behaviours;
using WashingMachineManagementApi.Application.Common.Services;

namespace WashingMachineManagementApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddLocalization(configuration)
            .AddFluentValidation()
            .AddAutoMapper()
            .AddMediatR()
            .AddMqttClientHostedService(configuration);

        return services;
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        // Adds the transient pipeline behavior and additionally registers all `IAuthorizationHandlers` for a given assembly
        services.AddMediatorAuthorization(
                Assembly.GetExecutingAssembly(),
                options => options.UseUnauthorizedResultHandlerStrategy(new CustomUnauthorizedResultHandler())
            );
        // TODO: Add authorization behaviours to IStreamPipelineBehavior
        // Register all `IAuthorizer` implementations for a given assembly
        services.AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            configuration.AddBehavior(typeof(IStreamPipelineBehavior<,>), typeof(ValidationStreamBehaviour<,>));
        });

        return services;
    }

    private static IServiceCollection AddLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        var jsonLocalizationOptions = configuration.GetSection("Application").GetSection("Localization").Get<JsonLocalizationOptions>();
        var defaultRequestCulture =
            new RequestCulture(jsonLocalizationOptions.DefaultCulture, jsonLocalizationOptions.DefaultUICulture);
        var supportedCultures = jsonLocalizationOptions.SupportedCultureInfos.ToList();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = defaultRequestCulture;
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
            {
                var requestCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();
                return await Task.FromResult(new ProviderCultureResult(requestCulture));
            }));
        });

        services.AddJsonLocalization(options =>
        {
            options.CacheDuration = jsonLocalizationOptions.CacheDuration;
            options.ResourcesPath = jsonLocalizationOptions.ResourcesPath;
            options.LocalizationMode = AspNetCore.Localizer.Json.JsonOptions.LocalizationMode.I18n;
            options.FileEncoding = jsonLocalizationOptions.FileEncoding;
            options.DefaultCulture = jsonLocalizationOptions.DefaultCulture;
            options.DefaultUICulture = jsonLocalizationOptions.DefaultUICulture;
            options.SupportedCultureInfos = jsonLocalizationOptions.SupportedCultureInfos;
        });

        return services;
    }

    public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services, IConfiguration configuration)
    {
        var username = configuration.GetSection("Application").GetSection("MqttBroker").GetValue<string>("Username");
        var password = configuration.GetSection("Application").GetSection("MqttBroker").GetValue<string>("Password");
        var clientId = configuration.GetSection("Application").GetSection("MqttBroker").GetValue<string>("ClientId");
        var host = configuration.GetSection("Application").GetSection("MqttBroker").GetValue<string>("Host");
        var port = configuration.GetSection("Application").GetSection("MqttBroker").GetValue<int>("Port");

        services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
        {
            aspOptionBuilder
                .WithCredentials(username, password)
                .WithClientId(clientId)
                .WithTcpServer(host, port);
        });

        return services;
    }

    private static IServiceCollection AddMqttClientServiceWithConfig(
            this IServiceCollection services, Action<MqttClientOptionsBuilder> configure)
    {
        services.AddSingleton<MqttClientOptions>(serviceProvider =>
        {
            var optionBuilder = new MqttClientOptionsBuilder();
            configure(optionBuilder);
            return optionBuilder.Build();
        });

        services.AddScoped<MqttClientService>();
        // services.AddSingleton<IHostedService>(serviceProvider =>
        // {
        //     return serviceProvider.GetService<MqttClientService>();
        // });
        // services.AddSingleton<MqttClientServiceProvider>(serviceProvider =>
        // {
        //     var mqttClientService = serviceProvider.GetService<MqttClientService>();
        //     var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
        //     return mqttClientServiceProvider;
        // });
        return services;
    }
}
