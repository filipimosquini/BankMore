using Authentication.Application.Users.Commands.SignIn;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.Api.Documentation.Swagger.Examples;

public class SignInCommandExample : IExamplesProvider<SignInCommand>
{
    public SignInCommand GetExamples()
    => new()
    {
        Cpf = "20069640033",
        Password = "Teste@123"
    };
}