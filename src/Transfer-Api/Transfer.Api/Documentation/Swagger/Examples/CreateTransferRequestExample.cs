using Swashbuckle.AspNetCore.Filters;
using Transfer.Api.Controllers.Requests;

namespace Transfer.Api.Documentation.Swagger.Examples;

public class CreateTransferRequestExample : IExamplesProvider<CreateTransferRequest>
{
    public CreateTransferRequest GetExamples()
        => new(123, 300);
}