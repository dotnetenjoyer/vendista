using Vendista.Infrastructure.Abstractions.Services;
using Vendista.Infrastructure.Implementation.Services.Vendista;
using Vendista.UseCases.Common;
using Vendista.UseCases.Terminals.SearchCommands;

namespace Vendista.WebApp;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(SearchCommandsQuery).Assembly));

        services.AddAutoMapper(typeof(CommonProfile).Assembly);
        
        services.AddTransient<IVendistaClient, VendistaClient>(serviceProvider =>
        {
            var login = configuration["Vendista:login"];
            var password = configuration["Vendista:password"];
            return new VendistaClient(login, password);
        });
        
        // services.UseSerilog((context, services, configuration) =>
        // {
        //     var hostEnvironment = services.GetRequiredService<IWebHostEnvironment>();
        //     var logPath = Path.Combine(hostEnvironment.ContentRootPath, "Logs/log.txt");
        //
        //     configuration
        //         .ReadFrom.Configuration(context.Configuration, sectionName: "Serilog")
        //         .ReadFrom.Services(services)
        //         .Enrich.FromLogContext()
        //         .WriteTo.Console()
        //         .WriteTo.File(logPath, rollingInterval: RollingInterval.Day);
        // });
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    } 
}