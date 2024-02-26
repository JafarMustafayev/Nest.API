namespace Nest.Application.DTOs;

public class MailResponseForTableDTO
{
    public string MessageId { get; set; }
    public string? Body { get; set; }
    public string? Subject { get; set; }
    public string To { get; set; }
    public string From { get; set; }
    public DateTime Date { get; set; }
}