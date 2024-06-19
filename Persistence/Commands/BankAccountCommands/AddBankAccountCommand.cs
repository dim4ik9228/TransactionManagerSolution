using Domain.BankAccounts;

namespace Persistence.Commands.BankAccountCommands;

public sealed class AddBankAccountCommand(TransactionManagerDbContext dbContext)
{
    public async Task<Guid> Execute(BankAccount bankAccount)
    {
        var bankAccountEntity = new Entities.BankAccount
        {
            Id = bankAccount.AccountNumber,
            BalanceCents = bankAccount.BalanceCents
        };

        await dbContext.BankAccounts
            .AddAsync(bankAccountEntity);

        return bankAccountEntity.Id;
    }
}