using Application.Managers;
using Domain.Shared;
using DTOs.TransactionDTOs;
using MediatR;

namespace Application.Queries.GetTransactionsQuery;

internal sealed class GetTransactionQueryHandler(ITransactionsManager transactionsManager)
    : IRequestHandler<GetTransactionsQuery, Result<IEnumerable<TransactionDto>>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionsManager.GetTransactions(request.BankAccountId);

        return Result<IEnumerable<TransactionDto>>.Success(transactions);
    }
}