namespace Domain.Transactions;

public class Transaction(Guid id, TransactionType transactionType, int amountCents)
{
    public Guid Id { get; } = id;
    public TransactionType TransactionType { get; } = transactionType;
    public int AmountCents { get; } = amountCents;

    public static Transaction DepositTransaction(int amountCents) =>
        new(Guid.NewGuid(), TransactionType.Deposit, amountCents);

    public static Transaction WithdrawalTransaction(int amountCents) =>
        new(Guid.NewGuid(), TransactionType.Withdrawal, amountCents);
}