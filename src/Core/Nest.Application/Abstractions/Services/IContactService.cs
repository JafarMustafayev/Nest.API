namespace Nest.Application.Abstractions.Services;

public interface IContactService
{
    Task<ResponseDTO> GetSingleAsync(string id);

    Task<ResponseDTO> GetAll();

    Task<ResponseDTO> CreateAsync(ContactCreateDTO contactCreateDTO);

    Task ReadMessage(Contact contact);

    Task<ResponseDTO> DeleteAsync(string id);
}