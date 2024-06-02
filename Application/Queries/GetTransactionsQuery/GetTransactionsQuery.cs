using Application.DTOs;
using Domain.Shared;
using MediatR;

namespace Application.Queries.GetTransactionsQuery;

public sealed record GetTransactionsQuery(Guid BankAccountId) : IRequest<Result<IEnumerable<TransactionDto>>>;