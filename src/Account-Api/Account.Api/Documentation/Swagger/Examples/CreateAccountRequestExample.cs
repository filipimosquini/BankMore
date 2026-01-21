using Account.Api.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

internal class CreateAccountRequestExample : IExamplesProvider<CreateAccountRequest>
{
    public CreateAccountRequest GetExamples()
        => new ("José da Silva");
}