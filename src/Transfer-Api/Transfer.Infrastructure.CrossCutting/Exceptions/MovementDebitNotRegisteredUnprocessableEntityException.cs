using System.Net;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class MovementDebitNotRegisteredUnprocessableEntityException() : AppNotificationBaseException("MOVEMENT_DEBIT_NOT_REGISTERED", HttpStatusCode.UnprocessableEntity);