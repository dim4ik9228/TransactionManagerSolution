using Application.Managers;
using Domain.BankAccounts;
using Persistence;
using Persistence.Commands.BankAccountCommands;
using Persistence.Queries.BankAccountQueries;

namespace Services;

internal sealed class BankAccountsManager(
    GetBankAccountByIdQuery getBankAccountByIdQuery,
    AddBankAccountCommand addBankAccountCommand,
    UpdateBankAccountBalanceCommand updateBankAccountBalanceCommand,
    TransactionManagerDbContext dbContext
) : IBankAccountsManager
{
    public async Task<BankAccount?> GetBankAccountById(Guid accountId)
    {
        return await getBankAccountByIdQuery.Execute(accountId);
    }

    public async Task<Guid> AddBankAccount(BankAccount bankAccount)
    {
        var result = await addBankAccountCommand.Execute(bankAccount);
        await dbContext.SaveChangesAsync();
        return result;
    }

    public async Task UpdateBankAccountBalance(Guid accountId, int updatedBalanceCents)
    {
        var result = await updateBankAccountBalanceCommand.Execute(accountId, updatedBalanceCents);

        if (result == 0)
        {
            throw new KeyNotFoundException("Account with such id was not found");
        }
    }
}