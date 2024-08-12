using RabbitMQ_Lab.AppExtensions;
using RabbitMQ_Lab.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRabbitMQService();
builder.Services.AddLogging(log => log.AddJsonConsole());

var app = builder.Build();

app.AddEndpointApi();
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Lab v1"));
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.UseHttpsRedirection();
app.Run();