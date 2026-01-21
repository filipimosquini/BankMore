using Account.Infrastructure.CrossCutting.ResourcesCatalog.Models;
using System;
using System.Net;

namespace Account.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyNotificationBaseException(NotificationEnvelope envelope, HttpStatusCode statusCode) : Exception
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public NotificationEnvelope Envelope { get; } = envelope;
}