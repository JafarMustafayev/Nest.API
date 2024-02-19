using Microsoft.Extensions.Configuration;

namespace Nest.Infrastructure;

internal static class Configuration
{
    internal static IConfigurationSection MailSettings
    {
        get
        {
            ConfigurationManager configurationManagerForSecrets = new();
            configurationManagerForSecrets.SetBasePath("C:\\Users\\Jafar Mustafayev\\AppData\\Roaming\\Microsoft\\UserSecrets\\2d513fb5-dc44-4b1b-ac11-eba052e0be6a");
            configurationManagerForSecrets.AddJsonFile("Secrets.json", optional: true, reloadOnChange: true);
            var str = configurationManagerForSecrets.GetSection("MailSettings");

            if (str is not null)
            { return str; }
            else
            { throw new Exception(" Mail settings not found"); }
        }
    }
}