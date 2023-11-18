using Serilog;

namespace Vendista.WebApp;

/// <summary>
/// Entry point class.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            var hostEnvironment = services.GetRequiredService<IWebHostEnvironment>();
            var logPath = Path.Combine(hostEnvironment.ContentRootPath, "Logs/log.txt");
    
            configuration
                .ReadFrom.Configuration(context.Configuration, sectionName: "Serilog")
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day);
        });
        
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        startup.Configure(app, app.Environment);

        app.Run();
    }
}

