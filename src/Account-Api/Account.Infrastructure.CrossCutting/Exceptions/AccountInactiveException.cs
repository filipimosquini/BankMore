using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class AccountInactiveException() : AppNotificationBaseException("INACTIVE_ACCOUNT", HttpStatusCode.BadRequest);