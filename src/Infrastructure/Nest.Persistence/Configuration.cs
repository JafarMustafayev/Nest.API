namespace Nest.Persistence;

public static class Configuration
{
    internal static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManagerForSecrets = new();
            configurationManagerForSecrets.SetBasePath("C:\\Users\\Jafar Mustafayev\\AppData\\Roaming\\Microsoft\\UserSecrets\\2d513fb5-dc44-4b1b-ac11-eba052e0be6a");
            configurationManagerForSecrets.AddJsonFile("Secrets.json", optional: true, reloadOnChange: true);

            var test = configurationManagerForSecrets.GetConnectionString("DefaultConnection");

            if (test is not null)
            { return test; }
            else
            { throw new ConnectionCustomException(); }
        }
    }

    internal static IConfigurationSection JwtData
    {
        get
        {
            ConfigurationManager configurationManagerForSecrets = new();
            configurationManagerForSecrets.SetBasePath("C:\\Users\\Jafar Mustafayev\\AppData\\Roaming\\Microsoft\\UserSecrets\\2d513fb5-dc44-4b1b-ac11-eba052e0be6a");
            configurationManagerForSecrets.AddJsonFile("Secrets.json", optional: true, reloadOnChange: true);
            var jwt = configurationManagerForSecrets.GetSection("Jwt");

            if (jwt is not null)
            { return jwt; }
            else
            { throw new Exception("JWT settings not found"); }
        }
    }
}