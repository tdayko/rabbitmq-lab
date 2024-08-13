using RabbitMQ_Lab.AppExtensions;
using RabbitMQ_Lab.Endpoints;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRabbitMQService();
builder.Host.UseSerilog((ctx, cfg) =>
{
    var dir = "logs/RabbitMQ_Lab.json";
    var dsn = builder.Configuration.GetValue<string>("Sentry:Dsn");

    cfg.WriteTo.Console();
    cfg.WriteTo.File(new JsonFormatter(renderMessage: true), dir, rollingInterval: RollingInterval.Day);
    cfg.WriteTo.Sentry(options =>
    {
        options.Dsn = dsn;
        options.Debug = true;
        options.MinimumEventLevel = Serilog.Events.LogEventLevel.Debug;
        options.TracesSampleRate = 1.0;
    });
});

var app = builder.Build();

app.AddEndpointApi();
app.UseSwagger();
app.UseExceptionHandler("/error");
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Lab v1"));
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.UseHttpsRedirection();
app.Run();