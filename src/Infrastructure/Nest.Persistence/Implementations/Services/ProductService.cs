namespace Nest.Persistence.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductImageReadRepository _productImageReadRepository;
    private readonly IProductImageWriteRepository _productImageWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IStorageService _storageService;
    private readonly IVendorReadRepository _vendorReadRepository;

    public ProductService(IProductImageReadRepository productImageReadRepository,
                          IProductImageWriteRepository productImageWriteRepository,
                          IProductReadRepository productReadRepository,
                          IProductWriteRepository productWriteRepository,
                          IStorageService storageService,
                          IVendorReadRepository vendorReadRepository,
                          IMapper mapper)
    {
        _mapper = mapper;
        _productImageReadRepository = productImageReadRepository;
        _productImageWriteRepository = productImageWriteRepository;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _storageService = storageService;
        _vendorReadRepository = vendorReadRepository;
    }

    public async Task<ResponseDTO> GetAllProductsAsync(int page = 1, int take = 20)
    {
        Expression<Func<Product, object>> order = x => x.Name;
        Expression<Func<Product, bool>> expression = x => !x.IsDeleted && x.InStock;

        List<Expression<Func<Product, object>>> includes = new()
        {
            x=>x.ProductImages,
            x=>x.Vendor
        };

        var res = _productReadRepository.GetAllByExpression(expression, page, take, false, order, includes);

        var map = _mapper.Map<List<GetSingleProductForGrid>>(res);

        return new()
        {
            Errors = null,
            Message = "Products retrieved successfully",
            Payload = map,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public async Task<ResponseDTO> GetProductByIdAsync(string id)
    {
        Expression<Func<Product, bool>> expression = x => !x.IsDeleted && x.Id == id;

        List<Expression<Func<Product, object>>> includes = new()
        {
            x=>x.ProductImages,
            //x=>x.Vendor
        };

        List<(Expression<Func<Product, object>> include, Expression<Func<object, object>> clude)> thenIncludes = new()
        {
            (x => x.Vendor, x => ((Vendor)x).Products)
        };

        var res = await _productReadRepository.GetSingleByExpressionAsync(expression, false, includes);

        if (res == null)
        {
            throw new NotFoundCustomException("Product not found");
        }

        var map = _mapper.Map<GetSingleProduct>(res);

        var relatedProducts = _productReadRepository.GetAllByExpression(x => x.VendorId == res.VendorId && x.Id != res.Id && !x.IsDeleted, 1, 6, false, x => x.Name, includes);

        map.RelatedProducts = _mapper.Map<List<GetSingleProductForGrid>>(relatedProducts);

        return new()
        {
            Errors = null,
            Message = "Product retrieved successfully",
            Payload = map,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public async Task<ResponseDTO> SearchProducts(string query)
    {
        if (query.Length < 3)
        {
            return new()
            {
                Errors = null,
                Message = "Search query must be at least 3 characters long",
                Payload = null,
                StatusCode = StatusCodes.Status400BadRequest,
                Success = false
            };
        }

        Expression<Func<Product, bool>> expression = x => !x.IsDeleted && (x.Id.Contains(query) || x.Name.Contains(query));

        Expression<Func<Product, object>> order = x => x.Name;

        List<Expression<Func<Product, object>>> includes = new()
        {
            (x=>x.ProductImages)
        };

        var res = _productReadRepository.GetAllByExpression(expression, 1, 100, false, order, includes);

        var map = _mapper.Map<List<GetSingleProductForGrid>>(res);

        return new ResponseDTO
        {
            Payload = map,
            Message = "Products retrieved successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Errors = null
        };
    }

    public async Task<ResponseDTO> SearchVendorProducts(string query, string vendorId)
    {
        if (query.Length < 3 || vendorId.Length < 32)
        {
            if (query.Length < 3)
            {
                return new()
                {
                    Errors = null,
                    Message = "Search query must be at least 3 characters long",
                    Payload = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false
                };
            }
            else
            {
                return new()
                {
                    Errors = null,
                    Message = "Vendor Idis invalid",
                    Payload = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false
                };
            }
        }

        Expression<Func<Product, bool>> expression = x => !x.IsDeleted && x.VendorId == vendorId && (x.Id.Contains(query) || x.Name.Contains(query));

        Expression<Func<Product, object>> order = x => x.Name;

        List<Expression<Func<Product, object>>> includes = new()
        {
            (x=>x.ProductImages),
            (x=>x.Vendor)
        };

        var res = _productReadRepository.GetAllByExpression(expression, 1, 100, false, order, includes);

        var map = _mapper.Map<List<GetSingleProductForGrid>>(res);

        return new ResponseDTO
        {
            Payload = map,
            Message = "Products retrieved successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Errors = null
        };
    }

    public async Task<ResponseDTO> CreateProductAsync(ProductCreateDTO productCreateDTO)
    {
        var vendor = await _vendorReadRepository.GetByIdAsync(productCreateDTO.VendorId);
        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        var product = _mapper.Map<Product>(productCreateDTO);

        await _productWriteRepository.AddAsync(product);

        var productImages = new List<IFormFile>();
        productImages.Add(productCreateDTO.MainImage);

        await UploadFile(productImages, FileContainerNameConsts.ProductImages, true, product);

        if (productCreateDTO.OtherImages != null)
        {
            await UploadFile(productCreateDTO.OtherImages, FileContainerNameConsts.ProductImages, false, product);
        }
        await _productWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "Product created successfully",
            Payload = null,
            StatusCode = StatusCodes.Status201Created,
            Success = true
        };
    }

    public async Task<ResponseDTO> UpdateProductAsync(ProductUpdateDTO product)
    {
        var productDb = await _productReadRepository.GetByIdAsync(product.Id);

        if (productDb == null)
        {
            throw new NotFoundCustomException("Product not found");
        }

        var vendor = await _vendorReadRepository.GetByIdAsync(product.VendorId);
        if (vendor == null)
        {
            throw new NotFoundCustomException("Vendor not found");
        }

        _mapper.Map(product, productDb);
        productDb.UpdatedAt = DateTime.UtcNow;

        _productWriteRepository.Update(productDb);

        if (product.MainImage != null)
        {
            var productImage = await _productImageReadRepository.GetSingleByExpressionAsync(x => x.ProductId == product.Id && x.IsMain);

            if (productImage != null)
            {
                await _storageService.DeleteAsync(productImage.ImagePath);
                _productImageWriteRepository.Remove(productImage);
            }

            await UploadFile(new List<IFormFile> { product.MainImage }, FileContainerNameConsts.ProductImages, true, productDb);
        }

        if (product.OtherImages != null)
        {
            await UploadFile(product.OtherImages, FileContainerNameConsts.ProductImages, false, productDb);
        }

        await _productWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "Product updated successfully",
            Payload = null,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public async Task<ResponseDTO> DeleteProductAsync(string id)
    {
        List<Expression<Func<Product, object>>> includes = new()
        {
            x=>x.ProductImages
        };

        var product = await _productReadRepository.GetByIdAsync(id, true, includes);

        if (product == null)
        {
            throw new NotFoundCustomException("Product not found");
        }

        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;

        foreach (var image in product.ProductImages)
        {
            image.IsDeleted = true;
            image.DeletedAt = DateTime.UtcNow;
            _productImageWriteRepository.Update(image);
        }

        _productWriteRepository.Update(product);

        await _productWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "Product deleted successfully",
            Payload = null,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public async Task<ResponseDTO> DeleteProductImageAsync(string productId, string imageId)
    {
        var product = await _productReadRepository.GetByIdAsync(productId);
        if (product == null)
        {
            throw new NotFoundCustomException("Product not found");
        }

        var productImage = await _productImageReadRepository.GetSingleByExpressionAsync(x => x.ProductId == productId && x.Id == imageId && !x.IsMain);
        if (productImage == null)
        {
            throw new NotFoundCustomException("Image not found");
        }

        productImage.IsDeleted = true;
        productImage.DeletedAt = DateTime.UtcNow;
        _productImageWriteRepository.Update(productImage);

        await _productImageWriteRepository.SaveChangesAsync();
        return new()
        {
            Errors = null,
            Message = "Image deleted successfully",
            Payload = null,
            StatusCode = StatusCodes.Status200OK,
            Success = true
        };
    }

    public Task<ResponseDTO> HardDeleteProductAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task UploadFile(ICollection<IFormFile> files, string pathOrContainerName, bool IsMain, Product product)
    {
        foreach (var file in files)
        {
            (string FileName, string pathName) res = await _storageService.UploadAsync(pathOrContainerName, file);
            var productImage = new ProductImage
            {
                ImagePath = res.pathName,
                IsMain = IsMain,
                ProductId = product.Id,
                ImageName = file.FileName
            };

            await _productImageWriteRepository.AddAsync(productImage);
        }
    }
}