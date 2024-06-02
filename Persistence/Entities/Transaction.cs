using Domain.Transactions;

namespace Persistence.Entities;

public sealed class Transaction
{
    public Guid Id { get; init; }
    public TransactionType TransactionType { get; init; }
    public int AmountCents { get; init; }
    public BankAccount BankAccount { get; init; }
    public Guid BankAccountId { get; init; }
}