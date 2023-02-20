using System.IO;
using Microsoft.Extensions.Configuration;

public static class ConfigManager
{
    /// <summary>
    /// The configuration object used to store configuration values read from the appsettings.json file.
    /// </summary>
    private static IConfiguration _configuration;

    /// <summary>
    /// Initializes the ConfigManager class by reading configuration values from the appsettings.json file in the current directory.
    /// </summary>
    static ConfigManager()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        _configuration = builder.Build();
    }

    /// <summary>
    /// Retrieves the API key required to access the OpenWeather API from the appsettings.json configuration file.
    /// </summary>
    /// <returns>The API key as a string.</returns>
    /// <exception cref="ApplicationException">Thrown when the AppSettings:APIConfig:Key configuration setting is missing or empty.</exception>
    public static string GetKey()
    {
        string _key = _configuration.GetSection("AppSettings:APIConfig:Key").Value;
        if (string.IsNullOrEmpty(_key))
        {
            throw new ApplicationException("AppSettings:APIConfig:Key is missing in appsettings.json");
        }
        return _key;      
    }

    /// <summary>
    /// Retrieves the City required to access the OpenWeather API from the appsettings.json configuration file.
    /// </summary>
    /// <returns>The API City as a string.</returns>
    /// <exception cref="ApplicationException">Thrown when the AppSettings:APIConfig:City configuration setting is missing or empty.</exception>
    public static string GetCity()
    {
        string _city = _configuration.GetSection("AppSettings:APIConfig:City").Value;
        if (string.IsNullOrEmpty(_city))
        {
            throw new ApplicationException("AppSettings:APIConfig:City is missing in appsettings.json");
        }
        return _city;
    }
}
