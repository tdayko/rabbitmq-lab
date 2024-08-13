using RabbitMQ_Lab.AppExtensions;
using RabbitMQ_Lab.Endpoints;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRabbitMQService();
builder.Host.UseSerilog((ctx, cfg) => {
    cfg.WriteTo.Console();
    cfg.WriteTo.File(new JsonFormatter(),"logs/RabbitMQ_Lab.json", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

app.AddEndpointApi();   
app.UseSwagger();
app.UseExceptionHandler("/error");
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Lab v1"));
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.UseHttpsRedirection();
app.Run();