using Account.Api.Controllers.Requests;
using Account.Application.Movement.Commands.CreateMovement;
using Account.Core.MovementAggregate.Enumerators;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

public class CreateMovementCommandExample : IExamplesProvider<CreateMovementCommand>
{
    public CreateMovementCommand GetExamples()
        => new()
        {
            RequestId = "25561a63-fe01-41fb-bb97-87e4a9b64ac1",
            AccountNumber = 123,
            Amount = 300,
            MovementType = MovementTypeEnum.C
        };
}
