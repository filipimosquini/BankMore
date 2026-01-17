using Authentication.Application.Users.Commands.CreateUser;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.Api.Documentation.Swagger.Examples;

public class DefaultRequestExample : IExamplesProvider<object>
{
    public object GetExamples()
        => string.Empty;
}