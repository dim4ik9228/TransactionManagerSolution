using Application.Commands.CreateAccountCommand;
using Application.DTOs;
using Application.Queries.GetBankAccountQuery;
using Domain.BankAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class AccountsController(ISender sender) : BaseApiController()
{
    [HttpGet("{accountNumber:guid}")]
    public async Task<ActionResult<BankAccountDto>> GetBankAccount(Guid accountNumber)
    {
        return HandleResult(await sender.Send(new GetBankAccountQuery(accountNumber)));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAccount(CreateAccountCommand createAccountCommand)
    {
        return HandleResult(await sender.Send(createAccountCommand));
    }
}