namespace Application.DTOs;

public sealed record BankAccountDto(
    Guid AccountNumber,
    int BalanceCents
);