using Domain.BankAccounts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries.BankAccountQueries;

public sealed class GetBankAccountByIdQuery(TransactionManagerDbContext dbContext)
{
    public async Task<BankAccount?> Execute(Guid bankAccountNumber)
    {
        var bankAccountEntity = await dbContext.BankAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountNumber));

        return bankAccountEntity is null
            ? null
            : new BankAccount(
                bankAccountEntity.Id,
                bankAccountEntity.BalanceCents);
    }
}