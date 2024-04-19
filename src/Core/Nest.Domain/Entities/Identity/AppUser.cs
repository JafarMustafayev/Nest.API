namespace Nest.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string FullName { get; set; }

    public DateTime? RefreshTokenExpiredDate { get; set; }
    public string? RefreshToken { get; set; }

    public ICollection<Likes>? Likes { get; set; }

    public AppUser()
    {
        Id = Guid.NewGuid().ToString();
    }
}