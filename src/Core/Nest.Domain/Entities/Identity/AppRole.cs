namespace Nest.Domain.Entities.Identity;

public class AppRole : IdentityRole<string>
{
    public AppRole()
    {
        Id = Guid.NewGuid().ToString();
    }
}