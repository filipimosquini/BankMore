using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class AccountInactiveException() : AppNotificationBaseException("INACTIVE_ACCOUNT", HttpStatusCode.BadRequest);