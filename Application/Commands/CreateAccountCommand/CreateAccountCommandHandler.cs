using Application.Managers;
using Domain.BankAccounts;
using Domain.Shared;
using MediatR;

namespace Application.Commands.CreateAccountCommand;

internal sealed class CreateAccountCommandHandler(IBankAccountsManager bankAccountsManager)
    : IRequestHandler<CreateAccountCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = new BankAccount(Guid.NewGuid(), request.InitialBalanceCents);

        var result = await bankAccountsManager.AddBankAccount(bankAccount);

        return Result<Guid>.Success(result);
    }
}