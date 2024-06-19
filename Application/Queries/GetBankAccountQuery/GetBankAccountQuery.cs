using Domain.Shared;
using DTOs.BankAccountDTOs;
using MediatR;

namespace Application.Queries.GetBankAccountQuery;

public sealed record GetBankAccountQuery(Guid BankAccountId) : IRequest<Result<BankAccountDto>>;