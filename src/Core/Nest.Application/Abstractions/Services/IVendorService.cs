namespace Nest.Application.Abstractions.Services;

public interface IVendorService
{
    Task<ResponseDTO> GetAllVendorsAsync(int page = 1, int take = 20);

    Task<ResponseDTO> GetVendorByIdAsync(string id);

    Task<ResponseDTO> SearchVendors(string query);

    Task<ResponseDTO> CreateVendorAsync(VendorCreateDTO vendor);

    Task<ResponseDTO> UpdateVendorAsync(VendorUpdateDTO vendor);

    Task<ResponseDTO> DeleteVendorAsync(string id);

    Task<ResponseDTO> HardDeleteVendorAsync(string id);
}