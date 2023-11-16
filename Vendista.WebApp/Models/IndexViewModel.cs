using Vendista.Domain.Entities;

namespace Vendista.WebApp.Models;

public class IndexViewModel
{
    public IEnumerable<CommandType> CommandTypes { get; set; }
}