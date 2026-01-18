using Account.Core.MovementAggregate.Enumerators;
using Account.Infrastructure.CrossCutting.Entities;
using System;

namespace Account.Core.MovementAggregate;

public class Movement : BaseEntity
{
    public MovementTypeEnum MovementType { get; protected set; }
    public DateTime MovementDate { get; protected set; }
    public decimal Amount { get; protected set; }
    public Guid AccountId { get; protected set; }

    /* EF Relation */
    public AccountAggregate.Account Account { get; protected set; }

    protected Movement() { }

    public Movement(MovementTypeEnum movementType, decimal amount, Guid accountId)
    {
        MovementType = movementType;
        Amount = amount;
        AccountId = accountId;

        MovementDate = DateTime.Now;
    }
}
