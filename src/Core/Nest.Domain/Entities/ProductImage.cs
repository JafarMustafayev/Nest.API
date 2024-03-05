namespace Nest.Domain.Entities;

public class ProductImage : BaseEntity
{
    public string ImagePath { get; set; }

    public string ImageName { get; set; }

    public string ProductId { get; set; }

    public Product Product { get; set; }

    public bool IsMain { get; set; }
}