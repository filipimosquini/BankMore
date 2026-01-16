using Authentication.Application.Users.Commands.Dto;
using MediatR;

namespace Authentication.Application.Users.Commands.SignIn;

public class SignInCommand : IRequest<AuthenticationDto>
{
    /// <summary>
    /// The Cpf property
    /// </summary>
    public string Cpf { get; set; }

    /// <summary>
    /// The password property
    /// </summary>
    public string Password { get; set; }
}