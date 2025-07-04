using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace SnowApi.DbUp;

public static class Program
{
    private const string DatabaseName = "SnowDatabase";

    public static int Main(string[] args)
    {
        var runtimeEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? throw new InvalidOperationException("settings not found for 'DOTNET_ENVIRONMENT'");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{runtimeEnvironment}.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = GetDevelopmentConnectionString(configuration);

        var upgradeEngine = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgradeEngine.PerformUpgrade();
        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            Console.ReadLine();
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }

    private static string GetDevelopmentConnectionString(IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString(DatabaseName);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string not found.");
        }
        EnsureDatabase.For.SqlDatabase(connectionString);

        return connectionString;
    }
}
