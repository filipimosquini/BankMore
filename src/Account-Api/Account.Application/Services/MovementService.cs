using Account.Core.MovementAggregate.Enumerators;
using Account.Core.MovementAggregate.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Account.Application.Services;

public class MovementService(IMovementRepository movementRepository) : IMovementService
{
    public async Task<decimal> GetBalanceAsync(Guid accountId)
    {
        var movements = await movementRepository.GetByAccountIdAsync(accountId);

        if (movements is null || (movements is not null && !movements.Any()))
        {
            return 0.0M;
        }

        var credits = movements
            .Where(x => x.MovementType == MovementTypeEnum.C)
            .Sum(x => x.Amount);

        var debits = movements
            .Where(x => x.MovementType == MovementTypeEnum.D)
            .Sum(x => x.Amount);

        return Math.Round((credits - debits), 2);
    }
}