using Application.Commands.WithdrawCommand;
using Application.Managers;
using Domain.BankAccounts;
using NSubstitute.ReturnsExtensions;

namespace ApplicationUnitTests.Commands;

public sealed class WithdrawCommandTests
{
    private readonly Guid _bankAccountId = Guid.NewGuid();
    private readonly ITransactionsManager _transactionManager = Substitute.For<ITransactionsManager>();
    private readonly IBankAccountsManager _bankAccountManager = Substitute.For<IBankAccountsManager>();

    [Fact]
    public async Task WithdrawCommandHandler_ShouldWithdrawFundsFromBankAccountAndAddTransactionAndReturnSuccessResult()
    {
        // Arrange
        const int initialBalanceCents = 15000;
        const int withdrawAmountCents = 10000;
        var bankAccount = new BankAccount(_bankAccountId, initialBalanceCents);

        _bankAccountManager
            .GetBankAccountById(_bankAccountId)
            .Returns(bankAccount);

        var command = new WithdrawCommand(_bankAccountId, withdrawAmountCents);
        var handler = new WithdrawCommandHandler(_bankAccountManager, _transactionManager);

        // Act
        var result = await handler.Handle(command, default);

        result.IsSuccess
            .Should()
            .BeTrue();

        await _bankAccountManager.Received()
            .GetBankAccountById(_bankAccountId);

        await _transactionManager.ReceivedWithAnyArgs()
            .AddTransaction(null!, _bankAccountId);
    }

    [Fact]
    public async Task WithdrawCommandHandler_ShouldReturnFailureResult_WhenBankAccountReturnFailure()
    {
        // Arrange
        const int initialBalanceCents = 15000;
        const int withdrawAmountCents = 20000;
        var bankAccount = new BankAccount(_bankAccountId, initialBalanceCents);

        _bankAccountManager
            .GetBankAccountById(_bankAccountId)
            .Returns(bankAccount);

        var command = new WithdrawCommand(_bankAccountId, withdrawAmountCents);
        var handler = new WithdrawCommandHandler(_bankAccountManager, _transactionManager);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess
            .Should()
            .BeFalse();
    }

    [Fact]
    public async Task WithdrawCommandHandler_ShouldReturnFailureResult_WhenBankAccountIsNotFound()
    {
        _bankAccountManager.GetBankAccountById(_bankAccountId)
            .ReturnsNull();

        var command = new WithdrawCommand(_bankAccountId, default);
        var handler = new WithdrawCommandHandler(_bankAccountManager, _transactionManager);

        // Act
        var result = await handler.Handle(command, default);

        result.IsSuccess
            .Should()
            .BeFalse();

        result.Error
            .Should()
            .Be(BankAccountErrors.NotFound);
    }
}