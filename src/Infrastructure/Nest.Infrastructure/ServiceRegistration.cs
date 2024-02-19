﻿namespace Nest.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IMailService, MailService>();
        services.Configure<MailSettings>(Configuration.MailSettings);
        services.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection service) where T : class, IStorage
    {
        service.AddScoped<IStorage, T>();
    }
}