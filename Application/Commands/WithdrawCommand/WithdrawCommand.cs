using Domain.Shared;
using DTOs.TransactionDTOs;
using MediatR;

namespace Application.Commands.WithdrawCommand;

public sealed record WithdrawCommand(Guid BankAccountId, int AmountCents) : IRequest<Result<TransactionDto>>;