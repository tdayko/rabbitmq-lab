using RabbitMQ_Lab.AppExtensions;
using RabbitMQ_Lab.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ__HOST") ?? builder.Configuration.GetValue<string>("RabbitMQ:Host");
builder.Services.AddRabbitMQService(rabbitMqHost!);
var sentryDsn = Environment.GetEnvironmentVariable("SENTRY__DSN") ?? builder.Configuration.GetValue<string>("Sentry:Dsn");
builder.Host.AddLoggingConfiguration(sentryDsn!);

var app = builder.Build();

app.AddEndpointApi();
app.UseSwagger();
app.UseExceptionHandler("/error");
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Lab v1"));
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.UseHttpsRedirection();
app.Run();