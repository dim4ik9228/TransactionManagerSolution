using Application.Commands.RemoveTransactionCommand;
using Application.Managers;

namespace ApplicationUnitTests.Commands;

public sealed class RemoveTransactionCommandTests
{
    private readonly ITransactionsManager _transactionsManager = Substitute.For<ITransactionsManager>();
    private readonly Guid _transactionId = Guid.NewGuid();

    [Fact]
    public async Task RemoveTransactionCommandHandler_ShouldRemoveTransaction()
    {
        // Arrange
        var command = new RemoveTransactionCommand(_transactionId);
        var handler = new RemoveTransactionCommandHandler(_transactionsManager);

        // Act
        var result = await handler.Handle(command, default);

        result.IsSuccess
            .Should()
            .BeTrue();

        await _transactionsManager.Received()
            .RemoveTransaction(_transactionId);
    }
}