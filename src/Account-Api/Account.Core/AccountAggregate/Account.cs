using Account.Core.MovementAggregate;
using Account.Infrastructure.CrossCutting.Entities;
using System;
using System.Collections.Generic;

namespace Account.Core.AccountAggregate;

public class Account : BaseEntity
{
    public string Number { get; protected set; } = default!;
    public string Document { get; protected set; } = default!;
    public bool Active { get; protected set; }
    public Guid UserId { get; protected set; }


    /* EF Relation */
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();

    protected Account() { }

    public Account(string document, Guid userId)
    {
        Document = document;
        UserId = userId;

        GenerateSequenceNumber();
        EnableAccount();
    }

    private void GenerateSequenceNumber()
    {
        Number = new Random().Next(1, 100000).ToString();
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