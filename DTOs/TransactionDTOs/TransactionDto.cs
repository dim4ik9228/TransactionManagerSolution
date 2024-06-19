using Domain.Transactions;

namespace DTOs.TransactionDTOs;

public sealed record TransactionDto(
    Guid Id,
    TransactionType TransactionType,
    int AmountCents,
    Guid BankAccountId
);