using System.Net;
using System;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class AppNotificationBaseException : Exception
{
    public HttpStatusCode HttpStatusCode { get; }

    public AppNotificationBaseException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public AppNotificationBaseException(string message, HttpStatusCode httpStatusCode, Exception innerException) : base(message, innerException)
    {
        HttpStatusCode = httpStatusCode;
    }
}