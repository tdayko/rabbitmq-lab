using MassTransit;
using static RabbitMQ_Lab.Report.Report;

namespace RabbitMQ_Lab.Bus;

internal sealed class ConsumerReportRequestEvent(ILogger<ConsumerReportRequestEvent> logger) : IConsumer<ReportRequestEvent>
{
    private readonly ILogger<ConsumerReportRequestEvent> _logger = logger;
    public async Task Consume(ConsumeContext<ReportRequestEvent> context)
    {
        await Task.Delay(1000);
        var report = ReportList.RequestReports.FirstOrDefault(x => x.Id == context.Message.Id);
        if (report is null)
        {
            _logger.LogWarning($"Report with Id: {context.Message.Id} not found.");
            return;
        }

        report.Status = Status.Processing.ToString();
        _logger.LogInformation($"Processing report with Id: {report.Id} and Name: {report.Name}.");

        await Task.Delay(5000);

        report.Status = Status.Processed.ToString();
        report.ProcessedTime = DateTime.Now;
        _logger.LogInformation($"Report with Id: {report.Id} and Name: {report.Name} processed at: {report.ProcessedTime}.");
    }
}