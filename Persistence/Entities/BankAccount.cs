namespace Persistence.Entities;

internal sealed class BankAccount
{
    public Guid Id { get; init; }
    public int BalanceCents { get; set; }
    public ICollection<Transaction> Transactions { get; } = [];
}