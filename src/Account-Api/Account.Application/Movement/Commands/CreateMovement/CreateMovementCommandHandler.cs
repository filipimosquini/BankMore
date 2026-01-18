using System;
using Account.Application.Services;
using Account.Core.MovementAggregate.Enumerators;
using Account.Core.MovementAggregate.Repositories;
using Account.Infrastructure.CrossCutting.Exceptions;
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

        var accountFromLoggedUser = await accountService.ValidateAccountAsync(Guid.Parse(request.UserId));

        if (account.Number != accountFromLoggedUser.Number && request.MovementType == MovementTypeEnum.D)
        {
            throw new OnlyCreditMovementAcceptedConflictExcepton();
        }

        var movement =
            new Core.MovementAggregate.Movement(request.MovementType, request.Amount, account.Id);

        await movementRepository.AddAsync(movement);

        await movementRepository.UnitOfWork.Commit();

        return Unit.Value;
    }
}