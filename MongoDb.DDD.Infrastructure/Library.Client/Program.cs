using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Test.Client;
using Test.DAL;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // create service collection
        var services = new ServiceCollection();
        ConfigureServices(services);

        // create service provider
        var serviceProvider = services.BuildServiceProvider();

        // entry to run app
        await serviceProvider.GetService<App>().Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // configure logging
        services.AddLogging();
        services.AddOptions();

        // build config
        var confBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddEnvironmentVariables()
            .Build();

        services.Configure<BookstoreDatabaseSettings>(o => confBuilder.GetSection("BookServiceSettings"));

        // add services:
        services.AddTransient<BookService>();

        // add app
        services.AddTransient<App>();
    }
}