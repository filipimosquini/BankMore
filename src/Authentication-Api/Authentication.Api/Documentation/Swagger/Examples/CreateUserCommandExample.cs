using Authentication.Application.Users.Commands.CreateUser;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.Api.Documentation.Swagger.Examples;

public class CreateUserCommandExample : IExamplesProvider<CreateUserCommand>
{
    public CreateUserCommand GetExamples()
    => new()
    {
        Cpf = "20069640033",
        Password = "Teste@123",
        ConfirmPassword = "Teste@123"
    };
}