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

        var startup = new Startup();
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        startup.Configure(app, app.Environment);

        app.Run();
    }
}

