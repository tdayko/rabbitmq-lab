using RabbitMQ_Lab.API.Report;

namespace RabbitMQ_Lab.API.Endpoints;

public static class EndpointsApi
{
    public static IEndpointRouteBuilder AddEndpointApi(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("rabbitmq/api/");

        endpoints.MapPost("report/{name}", (string name) => Results.Ok(new RequestReport(name)));
        endpoints.MapGet("report", () => Results.Ok(ReportList.RequestReports));

        return endpoints;
    }
}