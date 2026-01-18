using System.ComponentModel;

namespace Account.Core.MovementAggregate.Enumerators;

public enum MovementTypeEnum
{
    [Description("Credit")]
    C = 'C',
    [Description("Debit")]
    D = 'D'
}