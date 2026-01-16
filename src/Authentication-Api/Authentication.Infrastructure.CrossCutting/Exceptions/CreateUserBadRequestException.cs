using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;

namespace Authentication.Infrastructure.CrossCutting.Exceptions;
public class CreateUserBadRequestException : IdentityErrorCustomException
{
    public CreateUserBadRequestException(IEnumerable<IdentityError> errors) : base(errors, HttpStatusCode.BadRequest)
    {
    }
}