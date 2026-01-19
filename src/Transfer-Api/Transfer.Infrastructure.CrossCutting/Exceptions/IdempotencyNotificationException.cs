using System;
using System.Net;
using Transfer.Infrastructure.CrossCutting.ResourcesCatalog.Models;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class IdempotencyNotificationException(NotificationEnvelope envelope, HttpStatusCode statusCode) : Exception
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public NotificationEnvelope Envelope { get; } = envelope;
}