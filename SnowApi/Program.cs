using Serilog;

namespace SnowApi;

public static class Program
{
    private static string? _runtimeEnvironment;
    private static IConfigurationRoot? _configuration;

    public static async Task Main(string[] args)
    {
        var errorLogLocation = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";
        var serilogSelfLogLocation = AppDomain.CurrentDomain.BaseDirectory + @"\SerilogSelfLog.txt";

        try
        {
            Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(File.CreateText(serilogSelfLogLocation)));

            var host = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    _runtimeEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                                          ?? throw new InvalidOperationException(
                                              "settings not found for 'ASPNETCORE_ENVIRONMENT'");

                    var config = context.Configuration;
                    _configuration = configurationBuilder.AddConfiguration(config).Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup(context => new Startup(_configuration!));
                })
                .Build();

            Log.Logger.Information("Application starting");
            await host.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Hard error. " + e.Message);
            Console.WriteLine(e.StackTrace);
            await File.AppendAllTextAsync(errorLogLocation,
                $"[{DateTime.Now}] Hard error: {e.Message}{Environment.NewLine}");
            await File.AppendAllTextAsync(errorLogLocation, $"[{DateTime.Now}] {e.StackTrace}{Environment.NewLine}");

            if (e.InnerException is not null)
            {
                Console.WriteLine(e.InnerException.Message);
                await File.AppendAllTextAsync(errorLogLocation,
                    $"[{DateTime.Now}] {e.InnerException.Message}{Environment.NewLine}");
            }

            if (e.InnerException?.InnerException is not null)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                await File.AppendAllTextAsync(errorLogLocation,
                    $"[{DateTime.Now}] {e.InnerException.InnerException.Message}{Environment.NewLine}");
            }

            if (e.InnerException?.InnerException?.InnerException is not null)
            {
                Console.WriteLine(e.InnerException.InnerException.InnerException.Message);
                await File.AppendAllTextAsync(errorLogLocation,
                    $"[{DateTime.Now}] {e.InnerException.InnerException.InnerException.Message}{Environment.NewLine}");
            }

            await Task.Delay(10000, CancellationToken.None).WaitAsync(CancellationToken.None);
            Environment.Exit(1);
        }
    }
}