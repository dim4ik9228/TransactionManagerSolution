using Domain.Shared;
using DTOs.TransactionDTOs;
using MediatR;

namespace Application.Queries.GetTransactionsQuery;

public sealed record GetTransactionsQuery(Guid BankAccountId) : IRequest<Result<IEnumerable<TransactionDto>>>;