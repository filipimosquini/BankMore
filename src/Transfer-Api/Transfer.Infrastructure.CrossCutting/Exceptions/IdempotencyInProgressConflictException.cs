using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyInProgressConflictException() : AppCustomException("IDEMPOTENCY_IN_PROGRESS", HttpStatusCode.Conflict);