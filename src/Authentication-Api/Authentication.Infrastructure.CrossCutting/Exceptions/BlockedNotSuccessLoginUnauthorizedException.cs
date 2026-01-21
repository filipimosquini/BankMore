using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class BlockedNotSuccessLoginUnauthorizedException : AppNotificationBaseException
{
    public BlockedNotSuccessLoginUnauthorizedException() : base("USER_UNAUTHORIZED", HttpStatusCode.Unauthorized)
    {
    }
}