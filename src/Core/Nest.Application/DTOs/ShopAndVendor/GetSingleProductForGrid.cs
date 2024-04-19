namespace Nest.Application.DTOs.ShopAndVendor;

public class GetSingleProductForGrid
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public double Discount { get; set; }

    public string VendorId { get; set; }

    public string VendorName { get; set; }

    public string ImageUrl { get; set; }

    public double Rating { get; set; } = 5;

    public bool IsNew { get; set; }
}