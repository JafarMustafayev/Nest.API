namespace Nest.Application.DTOs.ShopAndVendor;

public class GetSingleProduct
{
    public string Id { get; set; }

    public string Name { get; set; }

    public GetSingleVendorForGrid Vendor { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public double Discount { get; set; }

    public int Quantity { get; set; }

    public string SKU { get; set; }

    public bool InStock { get; set; }

    public string MainImageUrl { get; set; }

    public List<string> ImageUrls { get; set; }

    public List<GetSingleProductForGrid>? RelatedProducts { get; set; }
}