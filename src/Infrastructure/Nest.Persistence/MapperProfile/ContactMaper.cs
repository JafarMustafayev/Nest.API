namespace Nest.Persistence.MapperProfile;

public class ContactMaper : Profile
{
    public ContactMaper()
    {
        CreateMap<Contact, GetSingleContactDTO>().ReverseMap();
        CreateMap<ContactCreateDTO, Contact>().ReverseMap();
    }
}