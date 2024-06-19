using Microsoft.EntityFrameworkCore;

namespace Persistence.Commands.BankAccountCommands;

public sealed class UpdateBankAccountBalanceCommand(TransactionManagerDbContext dbContext)
{
    public async Task<int> Execute(Guid bankAccountId, int updatedBalanceCents)
    {
        return await dbContext
            .BankAccounts
            .Where(x => x.Id.Equals(bankAccountId))
            .ExecuteUpdateAsync(s => s.SetProperty(
                b => b.BalanceCents,
                b => updatedBalanceCents
            ));
    }
}