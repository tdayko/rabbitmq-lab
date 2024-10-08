namespace RabbitMQ_Lab.Report;

public class Report
{
    internal static class ReportList
    {
        public static List<ReportRequest> RequestReports = [];
    }

    internal sealed record ReportRequestEvent(Guid Id);

    public class ReportRequest(string name)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Status { get; set; } = Report.Status.Pending.ToString();
        public DateTime? ProcessedTime { get; set; } = null;
    }

    public enum Status
    {
        Pending,
        Processing,
        Processed,
        Failed
    }
}