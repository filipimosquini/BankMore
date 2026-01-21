using System.Net;
using System;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;

public class AppNotificationBaseException : Exception
{
    public HttpStatusCode HttpStatusCode { get; }

    public AppNotificationBaseException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}