using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedNotSuccessLoginUnauthorizedException : AppCustomException
{
    public BlockedNotSuccessLoginUnauthorizedException() : base("USER_UNAUTHORIZED", HttpStatusCode.Unauthorized)
    {
    }
}