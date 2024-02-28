namespace Nest.Application.Abstractions.Services;

public interface IRoleService
{
    Task<ResponseDTO> CreateAsync(string roleName);

    Task<ResponseDTO> GetSingleRoleAsync(string id);

    Task<ResponseDTO> GetAllRolesAsync();
}