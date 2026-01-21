using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class AccountNotFoundException() : AppNotificationBaseException("ACCOUNT_NOT_FOUND", HttpStatusCode.NotFound);