namespace Nest.Application.DTOs.ShopAndVendor;

public class GetSingleVendorForGrid
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public int ProductCount { get; set; }

    public string Description { get; set; }

    public double OverallRating { get; set; }

    public string ImageUrl { get; set; }

    public int Year { get; set; }
}