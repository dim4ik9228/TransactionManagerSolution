using Domain.BankAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Services;

internal sealed class BankAccountsManager(
    IServiceScopeFactory serviceScopeFactory
) : BaseSingletonService(serviceScopeFactory), IBankAccountsManager
{
    public async Task<BankAccount?> GetBankAccount(Guid accountId)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var bankAccount = await dbContext.BankAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(accountId));

        return bankAccount is null
            ? null
            : new BankAccount(bankAccount.Id, bankAccount.BalanceCents);
    }

    public async Task<Guid> AddBankAccount(BankAccount bankAccount)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var bankAccountEntity = new Persistence.Entities.BankAccount
        {
            Id = bankAccount.AccountNumber,
            BalanceCents = bankAccount.BalanceCents
        };

        await dbContext.BankAccounts.AddAsync(bankAccountEntity);
        await dbContext.SaveChangesAsync();

        return bankAccountEntity.Id;
    }

    public async Task UpdateBankAccountBalance(Guid accountId, int updatedBalanceCents)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var bankAccount = await dbContext.BankAccounts
            .FirstOrDefaultAsync(x => x.Id.Equals(accountId));

        if (bankAccount is null)
        {
            throw new KeyNotFoundException("Account with specified number was not found!");
        }

        bankAccount.BalanceCents = updatedBalanceCents;
        await dbContext.SaveChangesAsync();
    }
}