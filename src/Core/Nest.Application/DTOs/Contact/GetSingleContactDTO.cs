namespace Nest.Application.DTOs.Contact;

public class GetSingleContactDTO
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }
    public DateTime CreateDate { get; set; }
}