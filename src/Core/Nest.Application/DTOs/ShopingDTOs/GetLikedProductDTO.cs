namespace Nest.Application.DTOs.ShopingDTOs;

public class GetLikedProductDTO
{
    public string ProductName { get; set; } = string.Empty;

    public string ProductId { get; set; } = string.Empty;

    public string ProductImage { get; set; } = string.Empty;

    public double ProductPrice { get; set; }

    public double ProductDiscount { get; set; } = 0;
}