using Domain.BankAccounts;
using Domain.Transactions;

namespace DomainUnitTests;

public sealed class BankAccountWithdrawalTests
{
    private readonly BankAccount _bankAccount = new(Guid.NewGuid(), 15000);

    [Fact]
    public void Withdraw_ShouldReturnSuccessfulResult_WhenEnoughFundsOnBalance()
    {
        // Arrange
        const int withdrawAmount = 10000;
        const int expectedBalance = 5000;

        // Act
        var result = _bankAccount.Withdraw(withdrawAmount);

        // Assert
        result.IsSuccess
            .Should()
            .BeTrue();

        result.Value!.TransactionType
            .Should()
            .Be(TransactionType.Withdrawal);

        _bankAccount.BalanceCents
            .Should()
            .Be(expectedBalance);
    }

    [Fact]
    public void Withdraw_ShouldReturnFailureResult_WhenNotEnoughFundOnBalance()
    {
        // Arrange
        const int withdrawAmount = 20000;
        const int expectedBalance = 15000;

        // Act
        var result = _bankAccount.Withdraw(withdrawAmount);

        // Assert
        result.IsSuccess
            .Should()
            .BeFalse();

        result.Error
            .Should()
            .Be(BankAccountErrors.NotEnoughFunds);

        _bankAccount.BalanceCents
            .Should()
            .Be(expectedBalance);
    }
}