using Application.Commands.DepositCommand;
using Application.Commands.RemoveTransactionCommand;
using Application.Commands.WithdrawCommand;
using Application.DTOs;
using Application.Queries.GetTransactionsQuery;
using Domain.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class TransactionsController(ISender sender) : BaseApiController
{
    [HttpGet("{accountNumber:guid}")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions(Guid accountNumber)
    {
        return HandleResult(await sender.Send(new GetTransactionsQuery(accountNumber)));
    }

    [HttpPost("deposit/{accountNumber:guid}/{amountCents:int}")]
    public async Task<ActionResult<TransactionDto>> Deposit(Guid accountNumber, int amountCents)
    {
        return HandleResult(await sender.Send(new DepositCommand(accountNumber, amountCents)));
    }


    [HttpPost("withdraw/{accountNumber:guid}/{amountCents:int}")]
    public async Task<ActionResult<TransactionDto>> Withdraw(Guid accountNumber, int amountCents)
    {
        return HandleResult(await sender.Send(new WithdrawCommand(accountNumber, amountCents)));
    }

    [HttpDelete("{transactionId:guid}")]
    public async Task<ActionResult> RemoveTransaction(Guid transactionId)
    {
        return HandleResult(await sender.Send(new RemoveTransactionCommand(transactionId)));
    }
}