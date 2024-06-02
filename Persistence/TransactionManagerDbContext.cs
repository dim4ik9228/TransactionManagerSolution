using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence;

public class TransactionManagerDbContext(
    DbContextOptions<TransactionManagerDbContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; init; }
    public DbSet<Transaction> Transactions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}