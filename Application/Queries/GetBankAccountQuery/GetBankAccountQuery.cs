using Application.DTOs;
using Domain.Shared;
using MediatR;

namespace Application.Queries.GetBankAccountQuery;

public sealed record GetBankAccountQuery(Guid BankAccountId) : IRequest<Result<BankAccountDto>>;