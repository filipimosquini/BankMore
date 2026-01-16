using Authentication.Application.Users.Commands.Dto;
using MediatR;

namespace Authentication.Application.Users.Commands.SignIn;

public class SignInCommand : IRequest<AuthenticationDto>
{
    /// <summary>
    /// The Cpf property
    /// </summary>
    /// <example>20069640033</example>
    public string Cpf { get; set; }

    /// <summary>
    /// The password property
    /// </summary>
    /// <example> Teste@123 </example>
    public string Password { get; set; }
}