namespace RabbitMQ_Lab.API.Report;

internal static class ReportList
{
    public static List<RequestReport> RequestReports = [];
}

public class RequestReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public Status Status { get; set; } = Status.Pending;
    public DateTime? ProcessedTime { get; set; } = null;

    public RequestReport(string name)
    {
        Name = name;
        ReportList.RequestReports.Add(this);
    }
}

public enum Status
{
    Pending,
    Processing,
    Processed,
    Failed
}