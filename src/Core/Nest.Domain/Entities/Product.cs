namespace Nest.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public double Discount { get; set; }

    public int Quantity { get; set; }

    public string VendorId { get; set; }

    public Vendor Vendor { get; set; }

    public bool InStock { get; set; }

    public string SKU { get; set; }

    public ICollection<ProductImage>? ProductImages { get; set; }

    public ICollection<Likes>? Likes { get; set; }
}

//public ICollection<Review> Reviews { get; set; }