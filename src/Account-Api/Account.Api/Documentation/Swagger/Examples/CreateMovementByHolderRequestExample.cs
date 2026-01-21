using Account.Api.Controllers.Requests;
using Account.Core.MovementAggregate.Enumerators;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

internal class CreateMovementByHolderRequestExample : IExamplesProvider<CreateMovementByHolderRequest>
{
    public CreateMovementByHolderRequest GetExamples()
        => new()
        {
            Amount = 300,
            MovementType = MovementTypeEnum.C
        };
}
