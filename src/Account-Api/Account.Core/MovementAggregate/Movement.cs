using Account.Core.MovementAggregate.Enumerators;
using Account.Infrastructure.CrossCutting.Entities;
using System;

namespace Account.Core.MovementAggregate;

public class Movement : BaseEntity
{
    public MovementTypeEnum MovementType { get; protected set; }
    public DateTime MovementDate { get; protected set; }
    public decimal Value { get; set; }
    public Guid AccountId { get; protected set; }

    /* EF Relation */
    public AccountAggregate.Account Account { get; protected set; }

    protected Movement() { }
}
