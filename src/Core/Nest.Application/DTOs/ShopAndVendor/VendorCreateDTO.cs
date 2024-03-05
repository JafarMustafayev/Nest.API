namespace Nest.Application.DTOs.ShopAndVendor;

public class VendorCreateDTO
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public IFormFile? Image { get; set; }
}