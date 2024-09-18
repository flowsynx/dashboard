namespace Dashboard.Components.Pages.Logs;

public class LogItemModel
{
    public string? UserName { get; set; }
    public string? Machine { get; set; }
    public DateTime? TimeStamp { get; set; }
    public string? Message { get; set; }
    public string? Level { get; set; }
}