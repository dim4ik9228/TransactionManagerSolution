using API.Controllers;
using Application.Commands.DepositCommand;
using Application.Commands.RemoveTransactionCommand;
using Application.Commands.WithdrawCommand;
using Application.Queries.GetTransactionsQuery;
using Domain.Shared;
using Domain.Transactions;
using DTOs.TransactionDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitTests;

public class TransactionsControllerTests
{
    private readonly ISender _sender;
    private readonly TransactionsController _controller;

    public TransactionsControllerTests()
    {
        _sender = Substitute.For<ISender>();
        _controller = new TransactionsController(_sender);
    }

    [Fact]
    public async Task GetTransactions_ShouldSendCorrectQuery()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();

        var query = new GetTransactionsQuery(accountNumber);
        _sender.Send(query)
            .Returns(Result<IEnumerable<TransactionDto>>.Success([]));

        // Act
        await _controller.GetTransactions(accountNumber);

        // Assert
        await _sender
            .Received(1)
            .Send(query);
    }

    [Fact]
    public async Task GetTransactions_ShouldReturnTransactions_WhenExist()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var transactions = new List<TransactionDto>
        {
            new(Guid.NewGuid(), TransactionType.Deposit, 1000, accountNumber),
            new(Guid.NewGuid(), TransactionType.Withdrawal, 500, accountNumber)
        };

        _sender.Send(Arg.Any<GetTransactionsQuery>())
            .Returns(Result<IEnumerable<TransactionDto>>.Success(transactions));

        // Act
        var result = await _controller.GetTransactions(accountNumber);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<IEnumerable<TransactionDto>>>().Subject;
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedTransactions = okResult.Value.Should().BeOfType<List<TransactionDto>>().Subject;

        returnedTransactions.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public async Task Deposit_ShouldSendCorrectCommand()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var amountCents = 1000;

        _sender.Send(Arg.Is<DepositCommand>(cmd => cmd.BankAccountId == accountNumber
                                                   && cmd.AmountCents == amountCents))
            .Returns(Result<TransactionDto>.Success(default!));

        // Act
        await _controller.Deposit(accountNumber, amountCents);

        // Assert
        await _sender.Received(1)
            .Send(Arg.Is<DepositCommand>(cmd => cmd.BankAccountId == accountNumber
                                                && cmd.AmountCents == amountCents));
    }

    [Fact]
    public async Task Deposit_ShouldReturnOkResultWithTransaction_WhenSuccessful()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var amountCents = 1000;
        var expectedTransaction = new TransactionDto(
            Guid.NewGuid(),
            TransactionType.Deposit,
            amountCents,
            accountNumber);

        _sender.Send(Arg.Any<DepositCommand>())
            .Returns(Result<TransactionDto>.Success(expectedTransaction));

        // Act
        var result = await _controller.Deposit(accountNumber, amountCents);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<TransactionDto>>().Subject;
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(expectedTransaction);
    }

    [Fact]
    public async Task Withdraw_ShouldSendCorrectCommand()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var amountCents = 500;
        _sender.Send(Arg.Is<WithdrawCommand>(cmd => cmd.BankAccountId == accountNumber
                                                    && cmd.AmountCents == amountCents))
            .Returns(Result<TransactionDto>.Success(default!));
        // Act
        await _controller.Withdraw(accountNumber, amountCents);

        // Assert
        await _sender.Received(1)
            .Send(Arg.Is<WithdrawCommand>(cmd => cmd.BankAccountId == accountNumber
                                                 && cmd.AmountCents == amountCents));
    }

    [Fact]
    public async Task Withdraw_ShouldReturnOkResultWithTransaction_WhenSuccessful()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var amountCents = 500;

        var expectedTransaction = new TransactionDto(
            Guid.NewGuid(),
            TransactionType.Withdrawal,
            amountCents,
            accountNumber);

        _sender.Send(Arg.Is<WithdrawCommand>(cmd => cmd.BankAccountId == accountNumber
                                                    && cmd.AmountCents == amountCents))
            .Returns(Result<TransactionDto>.Success(expectedTransaction));

        // Act
        var result = await _controller.Withdraw(accountNumber, amountCents);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<TransactionDto>>().Subject;
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(expectedTransaction);
    }

    [Fact]
    public async Task RemoveTransaction_ShouldSendCorrectCommand()
    {
        // Arrange
        var transactionId = Guid.NewGuid();

        _sender.Send(Arg.Is<RemoveTransactionCommand>(cmd => cmd.TransactionId == transactionId))
            .Returns(Result<Unit>.Success(Unit.Value));
        // Act
        await _controller.RemoveTransaction(transactionId);

        // Assert
        await _sender.Received(1).Send(Arg.Is<RemoveTransactionCommand>(cmd => cmd.TransactionId == transactionId));
    }

    [Fact]
    public async Task RemoveTransaction_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        _sender.Send(Arg.Any<RemoveTransactionCommand>())
            .Returns(Result<Unit>.Success(Unit.Value));

        // Act
        var result = await _controller.RemoveTransaction(transactionId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
}