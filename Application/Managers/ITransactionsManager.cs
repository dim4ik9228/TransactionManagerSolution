using Domain.Transactions;
using DTOs.TransactionDTOs;

namespace Application.Managers;

public interface ITransactionsManager
{
    Task<List<TransactionDto>> GetTransactions(Guid bankAccountId);
    Task AddTransaction(Transaction transaction, Guid bankAccountId);
    Task RemoveTransaction(Guid transactionId);
}