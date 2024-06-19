using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence;

public class TransactionManagerDbContext(
    DbContextOptions<TransactionManagerDbContext> options) : DbContext(options)
{
    internal DbSet<BankAccount> BankAccounts { get; init; }
    internal DbSet<Transaction> Transactions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}