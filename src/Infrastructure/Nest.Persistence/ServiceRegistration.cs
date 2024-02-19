﻿

namespace Nest.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(ColorProfile));
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
            options.Password.RequiredLength = 4;
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
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
    }

    public static void AddWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
    }

    public static void AddServices(this IServiceCollection services)
    {
        
    }


}