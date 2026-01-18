using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyInProgressConflictException() : AppCustomException("IDEMPOTENCY_IN_PROGRESS", HttpStatusCode.Conflict);