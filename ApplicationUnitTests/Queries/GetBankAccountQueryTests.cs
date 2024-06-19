using Application.Managers;
using Application.Queries.GetBankAccountQuery;
using Domain.BankAccounts;
using DTOs.BankAccountDTOs;

namespace ApplicationUnitTests.Queries;

public sealed class GetBankAccountQueryTests
{
    private readonly IBankAccountsManager _bankAccountsManager = Substitute.For<IBankAccountsManager>();
    private readonly Guid _bankAccountId = Guid.NewGuid();

    [Fact]
    public async Task GetBankAccountQueryHandler_ShouldReturnBankAccount()
    {
        // Arrange
        var bankAccount = new BankAccount(_bankAccountId, default);

        _bankAccountsManager.GetBankAccountById(_bankAccountId)
            .Returns(bankAccount);

        var bankAccountDto = new BankAccountDto(bankAccount.AccountNumber, bankAccount.BalanceCents);

        var query = new GetBankAccountQuery(_bankAccountId);
        var handler = new GetBankAccountQueryHandler(_bankAccountsManager);

        // Act
        var result = await handler.Handle(query, default);

        result.IsSuccess
            .Should()
            .BeTrue();

        result.Value
            .Should()
            .Be(bankAccountDto);

        await _bankAccountsManager.Received()
            .GetBankAccountById(_bankAccountId);
    }
}