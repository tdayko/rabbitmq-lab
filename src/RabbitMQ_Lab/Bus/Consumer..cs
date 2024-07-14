using MassTransit;
using static RabbitMQ_Lab.Report.Report;

namespace RabbitMQ_Lab.Bus;

internal sealed class Consumer(Logger<Consumer> logger) : IConsumer<ReportRequestEvent>
{
    private readonly ILogger<Consumer> _logger = logger;
    public async Task Consume(ConsumeContext<ReportRequestEvent> context)
    {
        var report = ReportList.RequestReports.FirstOrDefault(x => x.Id == context.Message.Id);
        if (report is null)
        {
            _logger.LogInformation($"Report with Id {context.Message.Id} not found.");
            return;
        }

        report.Status = Status.Processing;
        _logger.LogInformation($"Processing report with Id {report.Id} and Name {report.Name}.");

        await Task.Delay(5000);

        report.Status = Status.Processed;
        report.ProcessedTime = DateTime.Now;
        _logger.LogInformation($"Report with Id {report.Id} and Name {report.Name} processed at {report.ProcessedTime}.");
    }
}