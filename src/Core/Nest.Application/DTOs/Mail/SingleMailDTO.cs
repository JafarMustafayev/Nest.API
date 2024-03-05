namespace Nest.Application.DTOs.Mail;

public class SingleMailDTO
{
    public string? To { get; set; }
    public string? From { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public string? ReplyTo { get; set; }
    public DateTime Date { get; set; }
    public string? MessageId { get; set; }
    public string? Importance { get; set; }
}