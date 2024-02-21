namespace Nest.Application;

public static class ServiceRegistrator
{
    [Obsolete]
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddFluentValidation(v =>
        {
            v.RegisterValidatorsFromAssemblyContaining<ContactCreateDTOValidator>();
        });

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(typeof(ContactCreateDTOValidator).Assembly);
    }
}