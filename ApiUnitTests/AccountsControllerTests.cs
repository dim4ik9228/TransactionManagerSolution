using API.Controllers;
using Application.Commands.CreateAccountCommand;
using Application.DTOs;
using Application.Queries.GetBankAccountQuery;
using Domain.BankAccounts;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitTests;

public class AccountsControllerTests
{
    private readonly ISender _sender;
    private readonly AccountsController _controller;

    public AccountsControllerTests()
    {
        _sender = Substitute.For<ISender>();
        _controller = new AccountsController(_sender);
    }


    [Fact]
    public async Task GetBankAccount_ShouldSendGetBankAccountCommand()
    {
        var accountId = Guid.NewGuid();
        var getAccountQuery = new GetBankAccountQuery(accountId);

        _sender.Send(getAccountQuery)
            .Returns(Result<BankAccountDto>.Success(new BankAccountDto(accountId, default)));

        // Act
        await _controller.GetBankAccount(accountId);

        await _sender.Received(1)
            .Send(getAccountQuery);
    }

    [Fact]
    public async Task GetBankAccount_ShouldReturnBankAccount_WhenAccountExists()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        var bankAccount = new BankAccountDto(accountNumber, 1000);
        _sender.Send(Arg.Any<GetBankAccountQuery>()).Returns(Result<BankAccountDto>.Success(bankAccount));

        // Act
        var result = await _controller.GetBankAccount(accountNumber);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<BankAccountDto>>().Subject;
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedAccount = okResult.Value.Should().BeOfType<BankAccountDto>().Subject;

        returnedAccount.Should().Be(bankAccount);
    }

    [Fact]
    public async Task GetBankAccount_ShouldReturnNotFound_WhenAccountDoesNotExist()
    {
        // Arrange
        var accountNumber = Guid.NewGuid();
        _sender.Send(Arg.Any<GetBankAccountQuery>())
            .Returns(Result<BankAccountDto>.Success(null!));

        // Act
        var result = await _controller.GetBankAccount(accountNumber);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<BankAccountDto>>().Subject;
        actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task CreateAccount_ShouldSendCreateAccountCommand()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var createAccountCommand = new CreateAccountCommand(1000);

        _sender.Send(createAccountCommand)
            .Returns(Result<Guid>.Success(accountId));

        // Act
        await _controller.CreateAccount(createAccountCommand);

        // Assert
        await _sender.Received()
            .Send(createAccountCommand);
    }

    [Fact]
    public async Task CreateAccount_ShouldReturnAccountId_WhenCreationIsSuccessful()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var createAccountCommand = new CreateAccountCommand(1000);
        _sender.Send(createAccountCommand).Returns(Result<Guid>.Success(accountId));

        // Act
        var result = await _controller.CreateAccount(createAccountCommand);

        // Assert
        var actionResult = result.Should().BeOfType<ActionResult<Guid>>().Subject;
        var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
        var returnedId = okResult.Value.Should().BeOfType<Guid>().Subject;

        returnedId.Should().Be(accountId);
    }
}