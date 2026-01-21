namespace Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;

public sealed record MovementRequest(int? AccountNumber, decimal Amount, char MovementType);

public sealed record MovementHolderRequest(decimal Amount, char MovementType);