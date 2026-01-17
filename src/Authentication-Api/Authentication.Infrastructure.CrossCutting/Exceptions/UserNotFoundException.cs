using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;

public class UserNotFoundException() : AppCustomException("USER_NOT_FOUND", HttpStatusCode.NotFound);