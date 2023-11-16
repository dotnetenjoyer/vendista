using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendista.UseCases.Terminals.SearchCommands;

namespace Vendista.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        await mediator.Send(new SearchCommandsQuery());
        return View();
    }
}