namespace Nest.Application.Abstractions.Services;

public interface IContactService
{
    Task<ResponseDTO> GetSingleAsync(string id);

    Task<ResponseDTO> GetAll();

    Task<ResponseDTO> CreateAsync(ContactCreateDTO contactCreateDTO);
}