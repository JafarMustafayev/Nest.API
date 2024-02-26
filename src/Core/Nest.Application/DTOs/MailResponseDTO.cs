using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nest.Application.DTOs;

public class MailResponseDTO
{
    public string? Body { get; set; }
    public string? Subject { get; set; }
    public string To { get; set; }
    public string From { get; set; }
    public string? CC { get; set; }
    public string? BCC { get; set; }
    public DateTime Date { get; set; }
    public List<string>? Attachments { get; set; }
}