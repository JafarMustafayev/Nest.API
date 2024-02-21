namespace Nest.Persistence.Implementations.Services;

public class ContactService : IContactService
{
    private readonly IContactWriteReposiyory _contactWriteReposiyory;
    private readonly IContactReadRepository _contactReadRepository;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public ContactService(IContactWriteReposiyory contactWriteReposiyory,
                          IContactReadRepository contactReadRepository,
                          IMapper mapper,
                          IMailService mailService)
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

    public Task<ICollection<GetSingleContactDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<GetSingleContactDTO> GetSingleAsync(string id)
    {
        throw new NotImplementedException();
    }
}