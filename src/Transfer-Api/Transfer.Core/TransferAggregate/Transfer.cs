using System;
using Transfer.Infrastructure.CrossCutting.Entities;

namespace Transfer.Core.TransferAggregate;

public class Transfer : BaseEntity
{
    public Guid SourceAccountId { get; protected set; }
    public Guid DestinationAccountId { get; protected set; }
    public DateTime TransferDate { get; protected set; }
    public decimal Amount { get; protected set; }

    protected Transfer() { }

    public Transfer(Guid sourceAccountId, Guid destinationAccountId, DateTime transferDate, decimal amount)
    {
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        TransferDate = transferDate;
        Amount = amount;
    }
}