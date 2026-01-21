using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedAttemptInvalidTooManyRequestsException : AppNotificationBaseException
{
    public BlockedAttemptInvalidTooManyRequestsException() : base("BLOCKED_INVALID_ATTEMPT", HttpStatusCode.TooManyRequests)
    {
    }
}