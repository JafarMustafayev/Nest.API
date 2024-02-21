namespace Nest.Application.Abstractions.Services;

public interface IContactService
{
    Task<GetSingleContactDTO> GetSingleAsync(string id);

    Task<ICollection<GetSingleContactDTO>> GetAll();

    Task<ResponseDTO> CreateAsync(ContactCreateDTO contactCreateDTO);
}