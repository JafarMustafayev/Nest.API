namespace Nest.Application.DTOs.ShopAndVendor;

public class ProductCreateDTO
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public double Discount { get; set; }

    public int Quantity { get; set; }

    public string? VendorId { get; set; }

    public bool InStock { get; set; }

    public string? SKU { get; set; }

    public IFormFile? MainImage { get; set; }

    public ICollection<IFormFile>? OtherImages { get; set; }
}