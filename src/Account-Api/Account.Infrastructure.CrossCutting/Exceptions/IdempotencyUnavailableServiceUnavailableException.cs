using System;
using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyUnavailableServiceUnavailableException(Exception innerException) : AppCustomException("IDEMPOTENCY_UNAVAILABLE", HttpStatusCode.ServiceUnavailable, innerException);