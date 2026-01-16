using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedNotAllowedLoginForbiddenException : AppCustomException
{
    public BlockedNotAllowedLoginForbiddenException() : base("BLOCKED_NOT_ALLOWED_LOGIN", HttpStatusCode.Forbidden)
    {
    }
}