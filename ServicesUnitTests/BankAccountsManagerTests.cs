using Domain.BankAccounts;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Persistence;
using Persistence.Commands.BankAccountCommands;
using Persistence.Queries.BankAccountQueries;
using Services;

namespace ServicesUnitTests;

public sealed class BankAccountsManagerTests
{
    private readonly TransactionManagerDbContext _dbContext;
    private readonly GetBankAccountByIdQuery _getBankAccountByIdQuery;
    private readonly AddBankAccountCommand _addBankAccountCommand;
    private readonly UpdateBankAccountBalanceCommand _updateBankAccountBalanceCommand;
    private readonly BankAccountsManager _bankAccountsManager;

    public BankAccountsManagerTests()
    {
        _dbContext = InstantiateDbContext();
        _getBankAccountByIdQuery = new GetBankAccountByIdQuery(_dbContext);
        _addBankAccountCommand = new AddBankAccountCommand(_dbContext);
        _updateBankAccountBalanceCommand = new UpdateBankAccountBalanceCommand(_dbContext);
        _bankAccountsManager = new BankAccountsManager(
            _getBankAccountByIdQuery,
            _addBankAccountCommand,
            _updateBankAccountBalanceCommand,
            _dbContext
        );
    }

    [Fact]
    public async Task GetBankAccountById_Should_CallGetBankAccountByIdQuery()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await _bankAccountsManager.GetBankAccountById(id);

        // Assert
        await _getBankAccountByIdQuery
            .Received(1)
            .Execute(id);
    }

    private static TransactionManagerDbContext InstantiateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TransactionManagerDbContext>();

        var options = optionsBuilder
            .UseInMemoryDatabase("test_db")
            .Options;

        return new TransactionManagerDbContext(options);
    }
}