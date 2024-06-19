namespace DTOs.BankAccountDTOs;

public sealed record BankAccountDto(
    Guid AccountNumber,
    int BalanceCents
);