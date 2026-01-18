using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class AccountNotFoundException() : AppCustomException("ACCOUNT_NOT_FOUND", HttpStatusCode.NotFound);