using System;
using Account.Infrastructure.CrossCutting.Entities;

namespace Account.Core.Common.Indepotencies.Entities;

public class Idempotency : BaseEntity
{
    public string RequestHash { get; protected set; }
    public string Result { get; protected set; }

    public Idempotency() { }

    public Idempotency(Guid id) : base(id) { }

    public void AddRequestHash(string requestHash)
    {
        RequestHash = requestHash;
    }

    public void AddResult(string result)
    {
        Result = result;
    }
}