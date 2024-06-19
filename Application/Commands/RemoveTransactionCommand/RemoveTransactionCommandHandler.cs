using Application.Managers;
using Domain.Shared;
using MediatR;

namespace Application.Commands.RemoveTransactionCommand;

internal sealed class RemoveTransactionCommandHandler(ITransactionsManager transactionsManager)
    : IRequestHandler<RemoveTransactionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
    {
        await transactionsManager.RemoveTransaction(request.TransactionId);

        return Result<Unit>.Success(Unit.Value);
    }
}