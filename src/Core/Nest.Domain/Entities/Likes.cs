namespace Nest.Domain.Entities;

public class Likes : BaseEntity
{
    public bool IsLike { get; set; } = false;

    public string UserId { get; set; }

    public string ProductId { get; set; }

    public AppUser User { get; set; }

    public Product Product { get; set; }
}