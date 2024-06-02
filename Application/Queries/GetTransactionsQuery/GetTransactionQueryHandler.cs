using Application.DTOs;
using Domain.Transactions;
using Domain.Shared;
using MediatR;

namespace Application.Queries.GetTransactionsQuery;

internal sealed class GetTransactionQueryHandler(ITransactionsManager transactionsManager)
    : IRequestHandler<GetTransactionsQuery, Result<IEnumerable<TransactionDto>>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionsManager.GetTransactions(request.BankAccountId);

        var transactionsToReturn = transactions
            .Select(x => new TransactionDto(
                x.Id,
                x.TransactionType,
                x.AmountCents,
                request.BankAccountId
            ));

        return Result<IEnumerable<TransactionDto>>.Success(transactionsToReturn);
    }
}