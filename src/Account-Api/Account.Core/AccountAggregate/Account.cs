using Account.Core.MovementAggregate;
using Account.Infrastructure.CrossCutting.Entities;
using System;
using System.Collections.Generic;

namespace Account.Core.AccountAggregate;

public class Account : BaseEntity
{
    public int Number { get; protected set; } = default!;
    public string Holder { get; protected set; } = default!;
    public bool Active { get; protected set; }
    public Guid UserId { get; protected set; }


    /* EF Relation */
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();

    protected Account() { }

    public Account(string holder, Guid userId)
    {
        Holder = holder;
        UserId = userId;

        GenerateSequenceNumber();
        EnableAccount();
    }

    private void GenerateSequenceNumber()
    {
        Number = new Random().Next(1, 100000);
    }

    public void EnableAccount()
    {
        Active = true;
    }
    public void DisableAccount()
    {
        Active = false;
    }
}