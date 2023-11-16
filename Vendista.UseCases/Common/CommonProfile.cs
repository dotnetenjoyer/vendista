using AutoMapper;

namespace Vendista.UseCases.Common;

/// <summary>
/// Contains common mapping configuration.
/// </summary>
public class CommonProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CommonProfile()
    {
        CreateMap(typeof(Infrastructure.Abstractions.Services.PagedList<>), typeof(PagedList<>));
    }
}