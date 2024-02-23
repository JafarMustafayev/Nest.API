namespace Nest.Persistence.MapperProfile;

public class ContactMaper : Profile
{
    public ContactMaper()
    {
        CreateMap<Contact, GetSingleContactDTO>().ReverseMap();
        CreateMap<Contact, GetSingleContactForTableDTO>().ReverseMap();
        CreateMap<ContactCreateDTO, Contact>().ReverseMap();
    }
}