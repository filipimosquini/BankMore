using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyKeyReuseConflictException() : AppCustomException("IDEMPOTENCY_KEY_REUSE", HttpStatusCode.Conflict);