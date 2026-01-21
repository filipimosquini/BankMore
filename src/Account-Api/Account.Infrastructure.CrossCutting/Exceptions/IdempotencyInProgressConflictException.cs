using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyInProgressConflictException() : AppNotificationBaseException("IDEMPOTENCY_IN_PROGRESS", HttpStatusCode.Conflict);