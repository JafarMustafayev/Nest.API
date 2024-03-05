namespace Nest.Persistence.MapperProfile;

public class VendorMapper : Profile
{
    public VendorMapper()
    {
        CreateMap<Vendor, GetSingleVendorForGrid>()
            .ForMember(des => des.ImageUrl, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(des => des.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
            .ForMember(des => des.ProductCount, opt => opt.MapFrom(src => src.Products.Count))
            .ReverseMap();

        CreateMap<Vendor, GetSingleVendor>()
            .ForMember(des => des.ImageUrl, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(des => des.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
            .ForMember(des => des.Products, opt => opt.MapFrom(src => src.Products))
            .ReverseMap();

        CreateMap<Vendor, VendorCreateDTO>().ReverseMap();

        CreateMap<Vendor, VendorUpdateDTO>().ReverseMap();
    }
}