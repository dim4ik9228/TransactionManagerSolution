using Application.Commands.CreateAccountCommand;
using Domain.BankAccounts;

namespace ApplicationUnitTests.Commands;

public sealed class CreateAccountCommandTests
{
    private readonly Guid _expectedAccountId = Guid.NewGuid();
    private readonly IBankAccountsManager _bankAccountsManager = Substitute.For<IBankAccountsManager>();

    [Fact]
    public async Task CreateAccountCommandHandler_ShouldCreateNewBankAccount()
    {
        // Arrange
        const int initialBalance = int.MaxValue;

        _bankAccountsManager.AddBankAccount(Arg.Any<BankAccount>())
            .ReturnsForAnyArgs(_expectedAccountId);

        var command = new CreateAccountCommand(initialBalance);
        var handler = new CreateAccountCommandHandler(_bankAccountsManager);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess
            .Should()
            .BeTrue();

        result.Value
            .Should()
            .Be(_expectedAccountId);

        await _bankAccountsManager.Received()
            .AddBankAccount(Arg.Any<BankAccount>());
    }
}