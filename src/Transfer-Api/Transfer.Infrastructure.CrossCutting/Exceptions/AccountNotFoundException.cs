using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class AccountNotFoundException() : AppNotificationBaseException("ACCOUNT_NOT_FOUND", HttpStatusCode.NotFound);