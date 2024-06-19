using Domain.BankAccounts;

namespace Application.Managers;

public interface IBankAccountsManager
{
    Task<BankAccount?> GetBankAccountById(Guid accountId);
    Task<Guid> AddBankAccount(BankAccount bankAccount);
    Task UpdateBankAccountBalance(Guid accountId, int updatedBalanceCents);
}