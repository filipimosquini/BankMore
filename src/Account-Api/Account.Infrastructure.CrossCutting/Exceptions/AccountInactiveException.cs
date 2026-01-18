using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class AccountInactiveException() : AppCustomException("INACTIVE_ACCOUNT", HttpStatusCode.BadRequest);