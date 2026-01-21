using Account.Api.Controllers.Requests;
using Account.Core.MovementAggregate.Enumerators;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

internal class CreateMovementRequestExample : IExamplesProvider<CreateMovementRequest>
{
    public CreateMovementRequest GetExamples()
        => new()
        {
            AccountNumber = 123,
            Amount = 300,
            MovementType = MovementTypeEnum.C
        };
}
