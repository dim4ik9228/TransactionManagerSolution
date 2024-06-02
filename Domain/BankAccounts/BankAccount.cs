using Domain.Shared;
using Domain.Transactions;

namespace Domain.BankAccounts;

public sealed class BankAccount(
    Guid accountNumber,
    int balanceCents)
{
    public Guid AccountNumber { get; } = accountNumber;
    public int BalanceCents { get; private set; } = balanceCents;

    public Result<Transaction> Withdraw(int amountCents)
    {
        if (BalanceCents < amountCents)
        {
            return Result<Transaction>.Failure(BankAccountErrors.NotEnoughFunds);
        }

        BalanceCents -= amountCents;

        return Result<Transaction>.Success(Transaction.WithdrawalTransaction(amountCents));
    }

    public Result<Transaction> Deposit(int amountCents)
    {
        BalanceCents += amountCents;

        return Result<Transaction>.Success(Transaction.DepositTransaction(amountCents));
    }
}