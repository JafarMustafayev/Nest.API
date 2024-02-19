namespace Nest.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection services)
    {
        services.AddTransient<IMailService, MailService>();
    }
}