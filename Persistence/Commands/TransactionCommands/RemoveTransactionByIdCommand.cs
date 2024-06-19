using Microsoft.EntityFrameworkCore;

namespace Persistence.Commands.TransactionCommands;

public sealed class RemoveTransactionByIdCommand(TransactionManagerDbContext dbContext)
{
    public async Task<int> Execute(Guid transactionId)
    {
        return await dbContext
            .Transactions
            .Where(x => x.Id.Equals(transactionId))
            .ExecuteDeleteAsync();
    }
}