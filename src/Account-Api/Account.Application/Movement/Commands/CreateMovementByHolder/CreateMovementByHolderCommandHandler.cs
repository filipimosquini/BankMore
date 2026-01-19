using Account.Application.Services;
using Account.Core.MovementAggregate.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Movement.Commands.CreateMovementByHolder;

public class CreateMovementByHolderCommandHandler(IAccountService accountService, IMovementRepository movementRepository) 
    : IRequestHandler<CreateMovementByHolderCommand, Unit>
{
    public async Task<Unit> Handle(CreateMovementByHolderCommand request, CancellationToken cancellationToken)
    {
        var account = await accountService.ValidateAccountAsync(Guid.Parse(request.UserId));

        var movement =
            new Core.MovementAggregate.Movement(request.MovementType, request.Amount, account.Id);

        await movementRepository.AddAsync(movement);

        await movementRepository.UnitOfWork.Commit(cancellationToken);

        return Unit.Value;
    }
}