using RabbitMQ_Lab.AppExtensions;
using RabbitMQ_Lab.Endpoints;

using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRabbitMQService();
builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) =>
{
    var dir = $"{Environment.CurrentDirectory}/log.json";

    lc.WriteTo.Console();
    lc.WriteTo.File(new JsonFormatter(), dir, fileSizeLimitBytes: 1024);
    lc.CreateLogger();
});

var app = builder.Build();

app.AddEndpointApi();
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Lab v1"));
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.UseHttpsRedirection();
app.Run();