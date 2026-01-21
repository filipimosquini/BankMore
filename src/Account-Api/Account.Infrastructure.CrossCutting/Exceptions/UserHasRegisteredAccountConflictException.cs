using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class UserHasRegisteredAccountConflictException() : AppNotificationBaseException("USER_HAS_REGISTERED_ACCOUNT", HttpStatusCode.Conflict);