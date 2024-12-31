using Elastic.Serilog.Sinks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Contracts.Services;
using Shared.Services;

namespace Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedServices(
        this WebApplicationBuilder builder,
        Action<IBusRegistrationContext,
        IRabbitMqBusFactoryConfigurator> configureConsumer)
    {
        var services = builder.Services;
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
                configureConsumer(ctx, cfg);
            });
        });


        builder.Host.UseSerilog((ctx, config) =>
        {
            config
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new[] { new Uri("http://localhost:9200") },
                opts =>
                {
                },
                transport =>
                {
                })
                .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                ;
        });

        services.AddSingleton(typeof(IEsoftLog<>), typeof(EsoftLog<>));
        services.AddScoped<ICQRSClient, CQRSClient>();

        return services;
    }
}