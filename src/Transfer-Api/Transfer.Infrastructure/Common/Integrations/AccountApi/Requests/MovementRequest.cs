using System;

namespace Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;

public sealed record MovementRequest(Guid RequestId, int? AccountNumber, decimal Amount, char MovementType);