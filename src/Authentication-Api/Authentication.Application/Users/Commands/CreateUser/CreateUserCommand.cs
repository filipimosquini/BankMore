using Authentication.Application.Users.Commands.Dto;
using MediatR;

namespace Authentication.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<AuthenticationDto>
{
    /// <summary>
    /// The CPF property
    /// </summary>
    /// <example>20069640033</example>
    public string Cpf { get; set; }

    /// <summary>
    /// The password property
    /// </summary>
    /// <example>Teste@123</example>
    public string Password { get; set; }

    /// <summary>
    /// The confirmed password property
    /// </summary>
    /// <example>Teste@123</example>
    public string ConfirmPassword { get; set; }
}