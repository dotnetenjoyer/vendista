using MediatR;

namespace Vendista.UseCases.Terminals.AddCommand;

/// <summary>
/// Handler for <see cref="AddCommandCommand"/>.
/// </summary>
internal class AddCommandCommandHandler : IRequestHandler<AddCommandCommand>
{
    /// <inheritdoc />
    public Task Handle(AddCommandCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}