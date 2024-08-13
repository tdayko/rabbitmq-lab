using MassTransit;

using RabbitMQ_Lab.Bus;

using Serilog;
using Serilog.Formatting.Json;

namespace RabbitMQ_Lab.AppExtensions;

internal static class AppExtensions
{
    public static IServiceCollection AddRabbitMQService(this IServiceCollection services, string rabitmqHost)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<ConsumerReportRequestEvent>();
            busConfigurator.UsingRabbitMq((context, configuration) =>
            {
                configuration.Host(new Uri(rabitmqHost), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                configuration.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IHostBuilder AddLoggingConfiguration(this IHostBuilder hostBuilder, string sentryDsn)
    {
        hostBuilder.UseSerilog((ctx, cfg) =>
        {
            var dir = "logs/RabbitMQ_Lab.json";

            cfg.WriteTo.Console();
            cfg.WriteTo.File(new JsonFormatter(renderMessage: true), dir, rollingInterval: RollingInterval.Day);
            cfg.WriteTo.Sentry(options =>
            {
                options.Dsn = sentryDsn;
                options.MinimumEventLevel = Serilog.Events.LogEventLevel.Debug;
                options.TracesSampleRate = 1.0;
            });
        });

        return hostBuilder;
    }
}