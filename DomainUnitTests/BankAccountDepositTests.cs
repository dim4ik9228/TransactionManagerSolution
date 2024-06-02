using Domain.BankAccounts;
using Domain.Transactions;

namespace DomainUnitTests;

public sealed class BankAccountDepositTests
{
    private readonly BankAccount _bankAccount = new(Guid.NewGuid(), 15000);

    [Fact]
    public void Deposit_ShouldAddPassedFundsAmountToBalance()
    {
        // Arrange
        const int depositAmountCents = 5000;
        const int expectedBalance = 20000;

        // Act
        var result = _bankAccount.Deposit(depositAmountCents);

        // Assert
        result.IsSuccess
            .Should()
            .BeTrue();

        result.Value!.TransactionType
            .Should()
            .Be(TransactionType.Deposit);

        _bankAccount.BalanceCents
            .Should()
            .Be(expectedBalance);
    }
}