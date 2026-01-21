using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class OnlyCreditMovementAcceptedConflictExcepton() : AppNotificationBaseException("ONLY_MOVEMENT_CREDIT_ACCEPTED", HttpStatusCode.Conflict);