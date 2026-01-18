using Account.Application.Services;
using Account.Core.MovementAggregate.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Movement.Commands.CreateMovement;

public class CreateMovementCommandHandler(IAccountService accountService, IMovementRepository movementRepository) 
    : IRequestHandler<CreateMovementCommand, Unit>
{
    public async Task<Unit> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
    {
        var account = await accountService.ValidateAccountAsync(request.AccountNumber);

        var movement =
            new Core.MovementAggregate.Movement(request.MovementType, request.Amount, account.Id);

        await movementRepository.AddAsync(movement);

        await movementRepository.UnitOfWork.Commit();

        return Unit.Value;
    }
}