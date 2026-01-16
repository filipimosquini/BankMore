using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedAttemptInvalidTooManyRequestsException : AppCustomException
{
    public BlockedAttemptInvalidTooManyRequestsException() : base("BLOCKED_INVALID_ATTEMPT", HttpStatusCode.TooManyRequests)
    {
    }
}