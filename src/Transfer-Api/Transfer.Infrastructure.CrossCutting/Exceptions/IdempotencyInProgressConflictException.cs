using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyInProgressConflictException() : AppNotificationBaseException("IDEMPOTENCY_IN_PROGRESS", HttpStatusCode.Conflict);