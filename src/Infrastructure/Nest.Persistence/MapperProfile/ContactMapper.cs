namespace Nest.Persistence.MapperProfile;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact, GetSingleContactDTO>().ReverseMap();
        CreateMap<Contact, GetSingleContactForTableDTO>().ReverseMap();
        CreateMap<ContactCreateDTO, Contact>().ReverseMap();
    }
}