using Application.DTOs;
using Domain.Shared;
using Domain.Transactions;
using MediatR;

namespace Application.Commands.DepositCommand;

public sealed record DepositCommand(Guid BankAccountId, int AmountCents) : IRequest<Result<TransactionDto>>;