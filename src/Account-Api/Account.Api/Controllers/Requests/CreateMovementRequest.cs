using Account.Core.MovementAggregate.Enumerators;

namespace Account.Api.Controllers.Requests;

public class CreateMovementRequest
{
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