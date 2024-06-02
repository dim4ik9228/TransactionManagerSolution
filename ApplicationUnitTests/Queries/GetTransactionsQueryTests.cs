using Application.DTOs;
using Application.Queries.GetTransactionsQuery;
using Domain.Transactions;

namespace ApplicationUnitTests.Queries;

public sealed class GetTransactionsQueryTests
{
    private readonly ITransactionsManager _transactionsManager = Substitute.For<ITransactionsManager>();
    private readonly Guid _accountId = Guid.NewGuid();

    [Fact]
    public async Task GetTransactionsQueryHandler_ShouldReturnTransactionsCollection()
    {
        // Arrange
        List<Transaction> expectedTransactionsList =
        [
            Transaction.DepositTransaction(500),
            Transaction.DepositTransaction(500),
            Transaction.WithdrawalTransaction(750)
        ];

        _transactionsManager.GetTransactions(_accountId)
            .Returns(expectedTransactionsList);

        var transactionDtos =
            expectedTransactionsList.Select(x =>
                new TransactionDto(x.Id, x.TransactionType, x.AmountCents, _accountId));

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
            .BeEquivalentTo(transactionDtos);

        await _transactionsManager.Received()
            .GetTransactions(_accountId);
    }
}