using System.Net;
using System;

namespace Transfer.Infrastructure.CrossCutting.Exceptions;

public class AppCustomException : Exception
{
    public HttpStatusCode HttpStatusCode { get; }

    public AppCustomException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}