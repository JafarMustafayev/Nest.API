namespace Nest.Persistence.Implementations.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<ResponseDTO> CreateAsync(string roleName)
    {
        var role = _roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            throw new DuplicateCustomException("Role already exists");
        }

        var result = await _roleManager.CreateAsync(new AppRole() { Name = roleName });

        if (result.Succeeded)
        {
            return new ResponseDTO
            {
                Message = "Role created successfully",
                StatusCode = 201
            };
        }
        throw new InvalidOperationCustomException("Role creation failed");
    }

    public Task<ResponseDTO> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> GetSingleRoleAsync(string id)
    {
        throw new NotImplementedException();
    }
}