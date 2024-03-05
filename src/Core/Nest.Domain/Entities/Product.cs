namespace Nest.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public string VendorId { get; set; }

    public Vendor Vendor { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
}

//public ICollection<Review> Reviews { get; set; }

//public ICollection<ProductImage> ProductImages { get; set; }
//public ICollection<ProductColor> ProductColors { get; set; }
//public ICollection<ProductSize> ProductSizes { get; set; }

//public ICollection<ProductType> ProductTypes { get; set; }
//public ICollection<ProductCategory> ProductCategories { get; set; }