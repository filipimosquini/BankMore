using System;

namespace Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;

public record InformationAccountResponse(Guid AccountId, int AccountNumber);

public record InformationAccountResponseEnvelope(InformationAccountResponse Data);