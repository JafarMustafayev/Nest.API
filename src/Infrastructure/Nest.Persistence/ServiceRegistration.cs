namespace Nest.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ContactMapper).Assembly);
        services.AddReadRepositories();
        services.AddServices();
        services.AddWriteRepositories();

        services.AddScoped<AppDbContext>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.ConnectionString);
        });

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContactReadRepository, ContactReadRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IVendorReadRepository, VendorReadRepository>();
    }

    public static void AddWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContactWriteReposiyory, ContactWriteRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IVendorWriteRepository, VendorWriteRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomMailService, CustomMailService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVendorService, VendorService>();
        services.AddScoped<IProductService, ProductService>();
    }
}