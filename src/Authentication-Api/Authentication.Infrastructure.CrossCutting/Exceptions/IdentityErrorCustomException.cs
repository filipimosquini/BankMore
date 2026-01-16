using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class IdentityErrorCustomException : Exception
{
    public HttpStatusCode HttpStatusCode { get; }
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityErrorCustomException(IEnumerable<IdentityError> errors, HttpStatusCode httpStatusCode) : base(BuildErrorMessage(errors))
    {
        HttpStatusCode = httpStatusCode;
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<IdentityError> errors)
    {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.Code}: {x.Description}");
        return string.Join(string.Empty, arr);
    }
}