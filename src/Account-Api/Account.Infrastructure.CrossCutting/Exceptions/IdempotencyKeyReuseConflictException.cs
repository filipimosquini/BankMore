using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyKeyReuseConflictException() : AppNotificationBaseException("IDEMPOTENCY_KEY_REUSE", HttpStatusCode.Conflict);