using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class SourceAndDestinationAccountAreEqualConflictException() : AppNotificationBaseException("SOURCE_DESTINATION_EQUALS", HttpStatusCode.Conflict);