using Microsoft.Extensions.Configuration;

public static class AppConfig
{
    public static IConfiguration Configuration { get; }

    static AppConfig()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static string GetConnectionString(string name)
    {
        return Configuration.GetConnectionString(name);
    }
}