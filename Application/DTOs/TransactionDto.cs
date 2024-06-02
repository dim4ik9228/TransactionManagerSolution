using Domain.Transactions;

namespace Application.DTOs;

public sealed record TransactionDto(
    Guid Id,
    TransactionType TransactionType,
    int AmountCents,
    Guid BankAccountId
);