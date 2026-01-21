using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class MovementCreditNotRegisteredUnprocessableEntityException() : AppNotificationBaseException("MOVEMENT_CREDIT_NOT_REGISTERED", HttpStatusCode.UnprocessableEntity);