namespace Domain.BankAccounts;

public interface IBankAccountsManager
{
    Task<BankAccount?> GetBankAccount(Guid accountId);
    Task<Guid> AddBankAccount(BankAccount bankAccount);
    Task UpdateBankAccountBalance(Guid accountId, int updatedBalanceCents);
}