﻿namespace Nest.Persistence.Implementations.Services;

public class ContactService : IContactService
{
    private readonly IContactWriteReposiyory _contactWriteReposiyory;
    private readonly IContactReadRepository _contactReadRepository;
    private readonly IMapper _mapper;
    private readonly ICustomMailService _mailService;

    public ContactService(IContactWriteReposiyory contactWriteReposiyory,
                          IContactReadRepository contactReadRepository,
                          IMapper mapper,
                          ICustomMailService mailService)
    {
        _contactWriteReposiyory = contactWriteReposiyory;
        _contactReadRepository = contactReadRepository;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<ResponseDTO> CreateAsync(ContactCreateDTO contactCreateDTO)
    {
        var contact = _mapper.Map<Contact>(contactCreateDTO);
        await _contactWriteReposiyory.AddAsync(contact);
        await _contactWriteReposiyory.SaveChangesAsync();

        await _mailService.SendEmailForContactAtMomentAsync(contactCreateDTO.Email, contactCreateDTO.Subject);
        return new()
        {
            Message = "Contact created successfully",
            Success = true,
            StatusCode = StatusCodes.Status201Created
        };
    }

    public async Task<ResponseDTO> DeleteAsync(string id)
    {
        var contact = await _contactReadRepository.GetByIdAsync(id);

        if (contact is null)
        {
            return new()
            {
                Message = "Contact not found",
                Success = false,
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        contact.IsDeleted = true;
        contact.DeletedAt = DateTime.UtcNow;
        _contactWriteReposiyory.Update(contact);
        await _contactWriteReposiyory.SaveChangesAsync();
        return new()
        {
            Message = "Contact deleted successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<ResponseDTO> GetAll(int page, int take)
    {
        Expression<Func<Contact, object>> orderBy = x => x.FullName;
        var contacts = _contactReadRepository.GetAllByExpression(x => !x.IsDeleted, page, take, false, orderBy);

        var map = _mapper.Map<ICollection<GetSingleContactForTableDTO>>(contacts);

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Payload = map,
            Message = "Contacts found",
            Success = true,
            Errors = null
        };
    }

    public async Task<ResponseDTO> GetSingleAsync(string id)
    {
        var contact = await _contactReadRepository.GetByIdAsync(id, false);

        if (contact is null)
        {
            return new()
            {
                Message = "Contact not found",
                Success = false,
                StatusCode = StatusCodes.Status404NotFound
            };
        }
        var map = _mapper.Map<GetSingleContactDTO>(contact);
        await ReadMessage(contact);

        return new()
        {
            Payload = map,
            Message = "Contact found",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task ReadMessage(Contact contact)
    {
        contact.IsRead = true;
        _contactWriteReposiyory.Update(contact);
        await _contactWriteReposiyory.SaveChangesAsync();
    }
}