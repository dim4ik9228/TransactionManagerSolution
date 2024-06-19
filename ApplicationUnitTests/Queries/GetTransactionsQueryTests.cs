using Application.Managers;
using Application.Queries.GetTransactionsQuery;
using Domain.Transactions;
using DTOs.TransactionDTOs;

namespace ApplicationUnitTests.Queries;

public sealed class GetTransactionsQueryTests
{
    private readonly ITransactionsManager _transactionsManager = Substitute.For<ITransactionsManager>();
    private readonly Guid _accountId = Guid.NewGuid();

    [Fact]
    public async Task GetTransactionsQueryHandler_ShouldReturnTransactionsCollection()
    {
        // Arrange
        List<TransactionDto> expectedTransactionsList =
        [
            new TransactionDto(Guid.NewGuid(), TransactionType.Deposit, 500, _accountId),
            new TransactionDto(Guid.NewGuid(), TransactionType.Deposit, 300, _accountId),
            new TransactionDto(Guid.NewGuid(), TransactionType.Withdrawal, 700, _accountId),
        ];

        _transactionsManager.GetTransactions(_accountId)
            .Returns(expectedTransactionsList);

        var query = new GetTransactionsQuery(_accountId);
        var handler = new GetTransactionQueryHandler(_transactionsManager);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess
            .Should()
            .BeTrue();

        result.Value
            .Should()
            .BeEquivalentTo(expectedTransactionsList);

        await _transactionsManager.Received()
            .GetTransactions(_accountId);
    }
}