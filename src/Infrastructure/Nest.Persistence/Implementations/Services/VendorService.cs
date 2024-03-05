namespace Nest.Persistence.Implementations.Services;

public class VendorService : IVendorService
{
    private readonly IMapper _mapper;
    private readonly IVendorReadRepository _vendorReadRepository;
    private readonly IVendorWriteRepository _vendorWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IStorageService _storageService;

    public VendorService(IMapper mapper,
                         IVendorReadRepository vendorReadRepository,
                         IVendorWriteRepository vendorWriteRepository,
                         IProductWriteRepository productWriteRepository,
                         IProductReadRepository productReadRepository,
                         IStorageService storageService)
    {
        _mapper = mapper;
        _vendorReadRepository = vendorReadRepository;
        _vendorWriteRepository = vendorWriteRepository;
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
    }

    public async Task<ResponseDTO> GetAllVendorsAsync(int page, int take)
    {
        Expression<Func<Vendor, object>> order = x => x.Name;

        Expression<Func<Vendor, bool>> expression = x => !x.IsDeleted;

        List<Expression<Func<Vendor, object>>> includes = new();

        includes.Add(x => x.Products);

        var res = _vendorReadRepository.GetAllByExpression(expression, page, take, false, order, includes);

        var map = _mapper.Map<List<GetSingleVendorForGrid>>(res);

        return new ResponseDTO
        {
            Payload = map,
            Message = "Vendors retrieved successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Errors = null
        };
    }

    public async Task<ResponseDTO> GetVendorByIdAsync(string id)
    {
        List<(Expression<Func<Vendor, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = new()
        {
            // (x => x., x => ((CarFeature)x).Feature),
            // (x => x.CarExtras, x => ((CarExtra)x).Extra),
        };

        var vendor = await _vendorReadRepository.GetByIdAsync(id, false, null, thenIncludes);

        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        var map = _mapper.Map<GetSingleVendor>(vendor);

        return new ResponseDTO
        {
            Payload = map,
            Message = "Vendor retrieved successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Errors = null
        };
    }

    public async Task<ResponseDTO> CreateVendorAsync(VendorCreateDTO createDTO)
    {
        var vendorName = await _vendorReadRepository.GetSingleByExpressionAsync(x => x.Name == createDTO.Name);
        var vendorEmail = await _vendorReadRepository.GetSingleByExpressionAsync(x => x.Email == createDTO.Email);

        if (vendorEmail != null || vendorName != null)
        {
            if (vendorName != null)
            {
                throw new DuplicateCustomException("Vendor name already exists");
            }
            else
            {
                throw new DuplicateCustomException("Vendor email already exists");
            }
        }

        var vendor = _mapper.Map<Vendor>(createDTO);

        (string fileName, string pathName) res = await _storageService.UploadAsync("Vendors", createDTO.Image);

        vendor.ImageName = res.fileName;
        vendor.ImagePath = res.pathName;

        await _vendorWriteRepository.AddAsync(vendor);

        await _vendorWriteRepository.SaveChangesAsync();

        return new ResponseDTO
        {
            Payload = null,
            Message = "Vendor created successfully",
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Errors = null
        };
    }

    public async Task<ResponseDTO> UpdateVendorAsync(VendorUpdateDTO updateDTO)
    {
        var vendor = await _vendorReadRepository.GetByIdAsync(updateDTO.Id);
        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        var vendorName = await _vendorReadRepository.GetSingleByExpressionAsync(x => x.Name == updateDTO.Name);
        var vendorEmail = await _vendorReadRepository.GetSingleByExpressionAsync(x => x.Email == updateDTO.Email);

        if (vendorEmail != null || vendorName != null)
        {
            if (vendorName != null)
            {
                throw new DuplicateCustomException("Vendor name already exists");
            }
            else
            {
                throw new DuplicateCustomException("Vendor email already exists");
            }
        }

        vendor = _mapper.Map(updateDTO, vendor);

        if (updateDTO.Image != null)
        {
            await _storageService.DeleteAsync("Vendors", vendor.ImageName);

            (string fileName, string pathName) res = await _storageService.UploadAsync("Vendors", updateDTO.Image);

            vendor.ImageName = res.fileName;
            vendor.ImagePath = res.pathName;
        }

        _vendorWriteRepository.Update(vendor);
        await _vendorWriteRepository.SaveChangesAsync();

        return new ResponseDTO
        {
            Payload = null,
            Message = "Vendor updated successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Errors = null
        };
    }

    public async Task<ResponseDTO> DeleteVendorAsync(string id)
    {
        List<Expression<Func<Vendor, object>>> includes = new();

        includes.Add(x => x.Products);

        var vendor = await _vendorReadRepository.GetByIdAsync(id, false, includes);

        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        vendor.IsDeleted = true;
        vendor.DeletedAt = DateTime.Now;

        foreach (var product in vendor.Products)
        {
            product.IsDeleted = true;
            product.DeletedAt = DateTime.Now;
            _productWriteRepository.Update(product);
        }
        _vendorWriteRepository.Update(vendor);
        await _vendorWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "The vendor was successfully deleted",
            Payload = null,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public async Task<ResponseDTO> HardDeleteVendorAsync(string id)
    {
        List<(Expression<Func<Vendor, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = new()
        {
            (x => x.Products, x => ((Product)x).ProductImages)
        };

        var vendor = await _vendorReadRepository.GetByIdAsync(id, false, null, thenIncludes);

        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        foreach (var product in vendor.Products)
        {
            foreach (var image in product.ProductImages)
            {
                await _storageService.DeleteAsync("Products", image.ImageName);
            }
        }

        _vendorWriteRepository.Remove(vendor);
        await _vendorWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "The vendor was successfully hard deleted",
            Payload = null,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }
}