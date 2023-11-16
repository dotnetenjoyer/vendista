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
            return new VendistaClient("user2", "password2");
        });
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    } 
}