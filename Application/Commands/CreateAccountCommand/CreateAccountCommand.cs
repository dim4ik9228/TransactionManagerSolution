using Domain.Shared;
using MediatR;

namespace Application.Commands.CreateAccountCommand;

public record CreateAccountCommand(int InitialBalanceCents) : IRequest<Result<Guid>>;