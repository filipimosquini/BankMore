using Account.Core.MovementAggregate.Enumerators;
using MediatR;

namespace Account.Application.Movement.Commands.CreateMovement;

public class CreateMovementCommand() : IRequest<Unit>
{
    /// <summary>
    /// The request Id used for idempotency verification.
    /// </summary>
    /// <example>25561a63-fe01-41fb-bb97-87e4a9b64ac1</example>
    public string RequestId { get; set; }

    /// <summary>
    /// The bank account number
    /// </summary>
    /// <example>123</example>
    public int AccountNumber { get; set; }

    /// <summary>
    /// The amount of bank movement
    /// </summary>
    /// <example>300</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// The Movement Types.
    /// The accepted values is [C] C or [D] D
    /// </summary>
    /// <example>C</example>
    public MovementTypeEnum MovementType { get; set; }
}