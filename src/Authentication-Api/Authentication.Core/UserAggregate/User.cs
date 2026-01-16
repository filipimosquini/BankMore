using System;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Core.UserAggregate;

public class User : IdentityUser
{
    public string Cpf { get; set; }
}