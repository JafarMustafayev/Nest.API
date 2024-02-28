namespace Nest.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime? RefreshTokenExpiredDate { get; set; }
    public string? RefreshToken { get; set; }

    public AppUser()
    {
        Id = Guid.NewGuid().ToString();
    }
}