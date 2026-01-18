using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyKeyReuseConflictException() : AppCustomException("IDEMPOTENCY_KEY_REUSE", HttpStatusCode.Conflict);