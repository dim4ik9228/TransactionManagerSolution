using Domain.Transactions;

namespace Persistence.Commands.TransactionCommands;

public sealed class AddTransactionCommand(TransactionManagerDbContext dbContext)
{
    public async Task Execute(Transaction transaction, Guid bankAccountId)
    {
        var transactionEntity = new Entities.Transaction
        {
            Id = transaction.Id,
            AmountCents = transaction.AmountCents,
            TransactionType = transaction.TransactionType,
            BankAccountId = bankAccountId
        };

        await dbContext.AddAsync(transactionEntity);
    }
}