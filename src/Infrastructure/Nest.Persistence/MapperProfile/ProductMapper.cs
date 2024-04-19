namespace Nest.Persistence.MapperProfile;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, GetSingleProduct>()
            .ForMember(des => des.MainImageUrl, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(x => x.IsMain).ImagePath))
            .ForMember(des => des.ImageUrls, opt => opt.MapFrom(src => src.ProductImages.Where(x => !x.IsMain).Select(x => x.ImagePath)))
            .ForMember(des => des.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ReverseMap();

        CreateMap<Product, GetSingleProductForGrid>()
            .ForMember(des => des.ImageUrl, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(x => x.IsMain).ImagePath))
            .ForMember(des => des.VendorName, opt => opt.MapFrom(src => src.Vendor.Name))
            .ForMember(des => des.VendorId, opt => opt.MapFrom(src => src.Vendor.Id))
            .ForMember(des => des.IsNew, opt => opt.MapFrom(src => src.CreatedAt.AddDays(7) > DateTime.UtcNow))
            .ReverseMap();

        CreateMap<Product, ProductCreateDTO>().ReverseMap();

        CreateMap<Product, ProductUpdateDTO>().ReverseMap();
    }
}