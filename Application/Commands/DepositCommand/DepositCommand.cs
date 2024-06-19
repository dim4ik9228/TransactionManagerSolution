using Domain.Shared;
using DTOs.TransactionDTOs;
using MediatR;

namespace Application.Commands.DepositCommand;

public sealed record DepositCommand(Guid BankAccountId, int AmountCents) : IRequest<Result<TransactionDto>>;