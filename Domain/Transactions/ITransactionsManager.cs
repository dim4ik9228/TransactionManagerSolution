namespace Domain.Transactions;

public interface ITransactionsManager
{
    Task<IEnumerable<Transaction>> GetTransactions(Guid bankAccountId);
    Task AddTransaction(Transaction transaction, Guid bankAccountId);
    Task RemoveTransaction(Guid transactionId);
}