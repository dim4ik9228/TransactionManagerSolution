namespace Persistence.Entities;

public sealed class BankAccount
{
    public Guid Id { get; init; }
    public int BalanceCents { get; set; }
    public ICollection<Transaction> Transactions { get; } = [];
}