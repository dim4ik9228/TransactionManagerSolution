using Domain.Shared;

namespace Domain.BankAccounts;

public static class BankAccountErrors
{
    public static readonly Error NotEnoughFunds =
        new("NotEnoughFunds", "There are not enough funds on this account");

    public static readonly Error NotFound = new("NotFound", "Account with specified ID is not found!");
}