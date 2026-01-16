using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedNotSuccessLoginUnauthorizedException : AppCustomException
{
    public BlockedNotSuccessLoginUnauthorizedException() : base("BLOCKED_NOT_SUCCESS_LOGIN", HttpStatusCode.Unauthorized)
    {
    }
}