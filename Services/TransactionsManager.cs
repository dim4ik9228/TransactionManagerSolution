using Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Services;

internal sealed class TransactionsManager(
    IServiceScopeFactory serviceScopeFactory)
    : BaseSingletonService(serviceScopeFactory), ITransactionsManager
{
    public async Task<IEnumerable<Transaction>> GetTransactions(Guid bankAccountId)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var transactions = await dbContext.Transactions
            .Where(x => x.BankAccountId.Equals(bankAccountId))
            .ToListAsync();

        return transactions.Select(x => new Transaction(x.Id, x.TransactionType, x.AmountCents));
    }

    public async Task AddTransaction(Transaction transaction, Guid bankAccountId)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var transactionEntity = new Persistence.Entities.Transaction
        {
            Id = transaction.Id,
            AmountCents = transaction.AmountCents,
            TransactionType = transaction.TransactionType,
            BankAccountId = bankAccountId
        };

        await dbContext.Transactions.AddAsync(transactionEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveTransaction(Guid transactionId)
    {
        var dbContext = GetRequiredService<TransactionManagerDbContext>();

        var transactionToRemove = await dbContext.Transactions
            .FirstOrDefaultAsync(x => x.Id.Equals(transactionId));

        if (transactionToRemove is null)
        {
            throw new KeyNotFoundException("Transaction with such Id was not found!");
        }

        dbContext.Transactions.Remove(transactionToRemove);
        await dbContext.SaveChangesAsync();
    }
}