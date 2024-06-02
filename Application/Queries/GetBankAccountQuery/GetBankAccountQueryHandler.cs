using Application.DTOs;
using Domain.BankAccounts;
using Domain.Shared;
using MediatR;

namespace Application.Queries.GetBankAccountQuery;

internal sealed class GetBankAccountQueryHandler(
    IBankAccountsManager bankAccountsManager)
    : IRequestHandler<GetBankAccountQuery, Result<BankAccountDto>>
{
    public async Task<Result<BankAccountDto>> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await bankAccountsManager.GetBankAccount(request.BankAccountId);

        if (bankAccount is null)
        {
            return Result<BankAccountDto>.Success(null!);
        }

        var bankAccountToReturn = new BankAccountDto(bankAccount.AccountNumber, bankAccount.BalanceCents);

        return Result<BankAccountDto>.Success(bankAccountToReturn);
    }
}