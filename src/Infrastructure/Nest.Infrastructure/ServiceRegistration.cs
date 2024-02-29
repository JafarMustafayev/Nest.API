namespace Nest.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomMailService, CustomMailService>();
        services.Configure<MailSettings>(Configuration.MailSettings);
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
    }

    public static void AddStorage<T>(this IServiceCollection service) where T : class, IStorage
    {
        service.AddScoped<IStorage, T>();
    }
}