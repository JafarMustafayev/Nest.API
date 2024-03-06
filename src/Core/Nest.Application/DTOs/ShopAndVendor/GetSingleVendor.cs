namespace Nest.Application.DTOs.ShopAndVendor;

public class GetSingleVendor
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public int Year { get; set; }

    public string ImageUrl { get; set; }

    public ICollection<GetSingleProductForGrid> Products { get; set; }
}