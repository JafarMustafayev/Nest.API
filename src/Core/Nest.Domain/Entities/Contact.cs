namespace Nest.Domain.Entities;

public class Contact : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadDate { get; set; }
}