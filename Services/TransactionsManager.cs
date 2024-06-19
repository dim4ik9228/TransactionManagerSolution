using Application.Managers;
using Domain.Transactions;
using DTOs.TransactionDTOs;
using Persistence;
using Persistence.Commands.TransactionCommands;
using Persistence.Queries.TransactionQueries;

namespace Services;

internal sealed class TransactionsManager(
    GetTransactionsByBankAccountIdQuery getTransactionsByBankAccountIdQuery,
    AddTransactionCommand addTransactionCommand,
    RemoveTransactionByIdCommand removeTransactionByIdCommand,
    TransactionManagerDbContext dbContext
) : ITransactionsManager
{
    public async Task<List<TransactionDto>> GetTransactions(Guid bankAccountId)
    {
        return await getTransactionsByBankAccountIdQuery.Execute(bankAccountId);
    }

    public async Task AddTransaction(Transaction transaction, Guid bankAccountId)
    {
        await addTransactionCommand.Execute(transaction, bankAccountId);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveTransaction(Guid transactionId)
    {
        var result = await removeTransactionByIdCommand.Execute(transactionId);

        if (result == 0)
        {
            throw new KeyNotFoundException("Transaction with such Id was not found!");
        }
    }
}