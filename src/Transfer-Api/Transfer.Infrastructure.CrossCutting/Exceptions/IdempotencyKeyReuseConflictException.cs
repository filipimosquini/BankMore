using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyKeyReuseConflictException() : AppNotificationBaseException("IDEMPOTENCY_KEY_REUSE", HttpStatusCode.Conflict);