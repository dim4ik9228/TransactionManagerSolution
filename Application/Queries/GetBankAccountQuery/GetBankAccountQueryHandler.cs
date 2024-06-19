using Application.Managers;
using Domain.Shared;
using DTOs.BankAccountDTOs;
using MediatR;

namespace Application.Queries.GetBankAccountQuery;

internal sealed class GetBankAccountQueryHandler(
    IBankAccountsManager bankAccountsManager)
    : IRequestHandler<GetBankAccountQuery, Result<BankAccountDto>>
{
    public async Task<Result<BankAccountDto>> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await bankAccountsManager.GetBankAccountById(request.BankAccountId);

        if (bankAccount is null)
        {
            return Result<BankAccountDto>.Success(null!);
        }

        var bankAccountToReturn = new BankAccountDto(bankAccount.AccountNumber, bankAccount.BalanceCents);

        return Result<BankAccountDto>.Success(bankAccountToReturn);
    }
}