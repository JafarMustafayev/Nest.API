namespace Nest.Application.Abstractions.Services;

public interface IProductService
{
    Task<ResponseDTO> GetAllProductsAsync(int page = 1, int take = 20);

    Task<ResponseDTO> GetProductByIdAsync(string id);

    Task<ResponseDTO> SearchProducts(string query);

    Task<ResponseDTO> SearchVendorProducts(string query, string vendorId);

    Task<ResponseDTO> CreateProductAsync(ProductCreateDTO product);

    Task<ResponseDTO> UpdateProductAsync(ProductUpdateDTO product);

    Task<ResponseDTO> DeleteProductImageAsync(string productId, string imageId);

    Task<ResponseDTO> DeleteProductAsync(string id);

    Task<ResponseDTO> HardDeleteProductAsync(string id);
}