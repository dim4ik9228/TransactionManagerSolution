using Domain.Shared;
using MediatR;

namespace Application.Commands.RemoveTransactionCommand;

public sealed record RemoveTransactionCommand(Guid TransactionId) : IRequest<Result<Unit>>;