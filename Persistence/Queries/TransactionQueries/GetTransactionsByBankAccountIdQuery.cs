using DTOs.TransactionDTOs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries.TransactionQueries;

public sealed class GetTransactionsByBankAccountIdQuery(TransactionManagerDbContext dbContext)
{
    public async Task<List<TransactionDto>> Execute(Guid bankAccountId)
    {
        return await dbContext
            .Transactions
            .AsNoTracking()
            .Where(t => t.BankAccountId.Equals(bankAccountId))
            .Select(t => new TransactionDto(
                t.Id,
                t.TransactionType,
                t.AmountCents,
                t.BankAccountId
            ))
            .ToListAsync();
    }
}