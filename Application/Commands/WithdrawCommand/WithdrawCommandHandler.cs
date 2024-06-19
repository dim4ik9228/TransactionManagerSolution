using Application.Managers;
using Domain.BankAccounts;
using Domain.Shared;
using DTOs.TransactionDTOs;
using MediatR;

namespace Application.Commands.WithdrawCommand;

internal sealed class WithdrawCommandHandler(
    IBankAccountsManager bankAccountsManager,
    ITransactionsManager transactionsManager) : IRequestHandler<WithdrawCommand, Result<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await bankAccountsManager.GetBankAccountById(request.BankAccountId);

        if (bankAccount is null)
        {
            return Result<TransactionDto>.Failure(BankAccountErrors.NotFound);
        }

        var result = bankAccount.Withdraw(request.AmountCents);

        if (!result.IsSuccess)
        {
            return Result<TransactionDto>.Failure(result.Error!);
        }

        await transactionsManager.AddTransaction(result.Value!, request.BankAccountId);
        await bankAccountsManager.UpdateBankAccountBalance(request.BankAccountId, bankAccount.BalanceCents);

        var transactionDto = new TransactionDto(
            Id: result.Value!.Id,
            TransactionType: result.Value.TransactionType,
            AmountCents: result.Value.AmountCents,
            BankAccountId: bankAccount.AccountNumber);

        return Result<TransactionDto>.Success(transactionDto);
    }
}