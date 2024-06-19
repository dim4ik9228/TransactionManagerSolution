using Application.Commands.DepositCommand;
using Application.Managers;
using Domain.BankAccounts;
using NSubstitute.ReturnsExtensions;

namespace ApplicationUnitTests.Commands;

public sealed class DepositCommandTests
{
    private readonly ITransactionsManager _transactionManager = Substitute.For<ITransactionsManager>();
    private readonly IBankAccountsManager _bankAccountManager = Substitute.For<IBankAccountsManager>();
    private readonly Guid _bankAccountId = Guid.NewGuid();

    [Fact]
    public async Task DepositCommandHandler_ShouldAddFundsToBankAccountAndAddTransactionAndReturnSuccessResult()
    {
        // Arrange
        const int initialBalanceCents = 15000;
        const int depositAmountCents = 10000;
        var bankAccount = new BankAccount(_bankAccountId, initialBalanceCents);

        _bankAccountManager
            .GetBankAccountById(_bankAccountId)
            .Returns(bankAccount);

        var command = new DepositCommand(_bankAccountId, depositAmountCents);
        var handler = new DepositCommandHandler(_transactionManager, _bankAccountManager);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess
            .Should()
            .BeTrue();

        await _bankAccountManager.Received()
            .UpdateBankAccountBalance(bankAccount.AccountNumber, bankAccount.BalanceCents);

        await _transactionManager.ReceivedWithAnyArgs()
            .AddTransaction(null!, _bankAccountId);
    }

    [Fact]
    public async Task DepositCommandHandler_ShouldReturnFailureResult_WhenAccountIsNotFound()
    {
        // Arrange

        _bankAccountManager.GetBankAccountById(_bankAccountId)
            .ReturnsNull();

        var command = new DepositCommand(_bankAccountId, default);
        var handler = new DepositCommandHandler(_transactionManager, _bankAccountManager);

        // Act
        var result = await handler.Handle(command, default);

        result.IsSuccess
            .Should()
            .BeFalse();

        result.Error
            .Should()
            .Be(BankAccountErrors.NotFound);

        await _bankAccountManager.Received()
            .GetBankAccountById(_bankAccountId);
    }
}