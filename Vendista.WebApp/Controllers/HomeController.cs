using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendista.UseCases.Terminals.SearchCommands;
using Vendista.WebApp.Models;

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
        var commandTypes = await mediator.Send(new SearchCommandsQuery());

        var viewModel = new IndexViewModel()
        {
            CommandTypes = commandTypes
        };
        
        return View(viewModel);
    }
}