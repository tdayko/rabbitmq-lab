using MassTransit;
using RabbitMQ_Lab.Bus;

namespace RabbitMQ_Lab.AppExtensions;

internal static class AppExtensions
{
    public static IServiceCollection AddRabbitMQService(this IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<Consumer>();
            busConfigurator.UsingRabbitMq((context, configuration) => configuration.Host(new Uri("amqp://localhost:5672"), host =>
            {
                host.Username("guest");
                host.Password("guest");
            }));
        });

        return services;
    }
}