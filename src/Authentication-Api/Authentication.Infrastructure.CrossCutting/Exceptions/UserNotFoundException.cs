using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;

public class UserNotFoundException() : AppNotificationBaseException("USER_NOT_FOUND", HttpStatusCode.NotFound);