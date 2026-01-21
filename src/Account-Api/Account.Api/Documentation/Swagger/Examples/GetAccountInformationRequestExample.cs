using Account.Api.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

internal class GetAccountInformationRequestExample() : IExamplesProvider<GetAccountInformationRequest>
{
    public GetAccountInformationRequest GetExamples()
        => new(123);
}