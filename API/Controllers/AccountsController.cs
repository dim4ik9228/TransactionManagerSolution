using Application.Commands.CreateAccountCommand;
using Application.Queries.GetBankAccountQuery;
using DTOs.BankAccountDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class AccountsController(ISender sender) : BaseApiController()
{
    [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status404NotFound)]
    [HttpGet("{accountNumber:guid}")]
    public async Task<ActionResult<BankAccountDto>> GetBankAccount(Guid accountNumber)
    {
        return HandleResult(await sender.Send(new GetBankAccountQuery(accountNumber)));
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAccount(CreateAccountCommand createAccountCommand)
    {
        return HandleResult(await sender.Send(createAccountCommand));
    }
}