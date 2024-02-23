namespace Nest.Application.DTOs.Contact;

public class GetSingleContactForTableDTO
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Subject { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}