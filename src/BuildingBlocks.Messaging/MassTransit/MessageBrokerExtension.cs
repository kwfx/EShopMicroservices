using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class MessageBrokerExtension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            if (assembly is not null)
            {
                cfg.AddConsumers(assembly);
            }
            cfg.UsingRabbitMq((context, rabbitMQConfig) =>
            {
                rabbitMQConfig.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:Username"]!);
                    host.Password(configuration["MessageBroker:password"]!);
                });
                rabbitMQConfig.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}