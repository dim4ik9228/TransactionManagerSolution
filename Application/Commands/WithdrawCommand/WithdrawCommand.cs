using Application.DTOs;
using Domain.Shared;
using MediatR;

namespace Application.Commands.WithdrawCommand;

public sealed record WithdrawCommand(Guid BankAccountId, int AmountCents) : IRequest<Result<TransactionDto>>;