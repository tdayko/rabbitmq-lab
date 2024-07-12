using MassTransit;
namespace RabbitMQ_Lab.Endpoints;

public static class EndpointsApi
{
    public static IEndpointRouteBuilder AddEndpointApi(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("rabbitmq/api/");

        endpoints.MapPost("report/{name}", async (string name, IBus bus) =>
        {
            // create report request
            var reportRequest = new Report.Report.ReportRequest(name);

            // add report requert for database
            Report.Report.ReportList.RequestReports.Add(reportRequest);
            // publish report request event to consumer 
            await bus.Publish(new Report.Report.ReportRequestEvent(reportRequest.Id));
            return Results.Accepted(value: reportRequest);
        });

        endpoints.MapGet("report", () => Results.Ok(Report.Report.ReportList.RequestReports));

        return endpoints;
    }
}