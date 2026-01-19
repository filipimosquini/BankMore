using System;
using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyUnavailableServiceUnavailableException(Exception innerException) : AppCustomException("IDEMPOTENCY_UNAVAILABLE", HttpStatusCode.ServiceUnavailable, innerException);